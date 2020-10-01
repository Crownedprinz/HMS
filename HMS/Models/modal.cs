using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace HMS.Models
{

    public class DecimalModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            ModelState modelState = new ModelState { Value = valueResult };
            object actualValue = null;
            try
            {
                actualValue = Convert.ToDecimal(valueResult.AttemptedValue,
                    CultureInfo.CurrentCulture);
            }
            catch (FormatException e)
            {
                modelState.Errors.Add(e);
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
        }
    }

    public class DefaultModelBinderWithHtmlValidation : DefaultModelBinder
    {

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            try
            {
                return base.BindModel(controllerContext, bindingContext);
            }
            catch (HttpRequestValidationException)
            {
                System.Diagnostics.Trace.TraceWarning("Illegal characters were found in field {0}", bindingContext.ModelMetadata.DisplayName ?? bindingContext.ModelName);
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, string.Format("Ilegal characters were found in field {0}", bindingContext.ModelMetadata.DisplayName ?? bindingContext.ModelName));
            }

            //Cast the value provider to an IUnvalidatedValueProvider, which allows to skip validation
            IUnvalidatedValueProvider provider = bindingContext.ValueProvider as IUnvalidatedValueProvider;
            if (provider == null) return null;

            //Get the attempted value, skiping the validation
            var result = provider.GetValue(bindingContext.ModelName, skipValidation: true);
            System.Diagnostics.Debug.Assert(result != null, "result is null");

            return result.AttemptedValue;
        }
    }


    public class BetterDefaultModelBinder : DefaultModelBinder
    {
        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            var model = base.CreateModel(controllerContext, bindingContext, modelType);

            if (model == null || model is IEnumerable)
                return model;

            foreach (var property in modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                object value = property.GetValue(model);
                if (value != null)
                    continue;

                if (property.PropertyType.IsArray)
                {
                    value = Array.CreateInstance(property.PropertyType.GetElementType(), 0);
                    property.SetValue(model, value);
                }
                else if (property.PropertyType.IsGenericType)
                {
                    Type typeToCreate;
                    Type genericTypeDefinition = property.PropertyType.GetGenericTypeDefinition();
                    if (genericTypeDefinition == typeof(IDictionary<,>))
                    {
                        typeToCreate = typeof(Dictionary<,>).MakeGenericType(property.PropertyType.GetGenericArguments());
                    }
                    else if (genericTypeDefinition == typeof(IEnumerable<>) ||
                             genericTypeDefinition == typeof(ICollection<>) ||
                             genericTypeDefinition == typeof(IList<>))
                    {
                        typeToCreate = typeof(List<>).MakeGenericType(property.PropertyType.GetGenericArguments());
                    }
                    else
                    {
                        continue;
                    }

                    value = Activator.CreateInstance(typeToCreate);
                    property.SetValue(model, value);
                }
            }

            return model;
        }
    }

}