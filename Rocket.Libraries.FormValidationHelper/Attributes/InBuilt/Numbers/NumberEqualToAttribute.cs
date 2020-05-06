namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Numbers
{
    public class NumberEqualToAttribute : NumberSizeValidator
    {
        public NumberEqualToAttribute (double number)
         : base (number, $"Value must be equal to '{number}'",NumberOperators.EqualTo)
         {
             
         }
    }
}