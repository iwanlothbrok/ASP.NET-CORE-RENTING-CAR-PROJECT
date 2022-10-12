namespace RentalCars.Core.ModelBinders
{
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
    public class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext
                             .ValueProvider
                             .GetValue(bindingContext.ModelName);

            if (valueResult != ValueProviderResult.None && String.IsNullOrWhiteSpace(valueResult.FirstValue) == false)
            {
                decimal actualValue = 0;
                bool succsess = false;

                try
                {
                    string decimalValue = valueResult.FirstValue;
                    decimalValue = decimalValue.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    decimalValue = decimalValue.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                    actualValue = Convert.ToDecimal(decimalValue, CultureInfo.CurrentCulture);
                    succsess = true;


                }
                catch (FormatException fe)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, fe, bindingContext.ModelMetadata);

                }

                if (succsess)
                {
                    bindingContext.Result = ModelBindingResult.Success(actualValue);

                }

            }
            return Task.CompletedTask;
        }
    }
}
