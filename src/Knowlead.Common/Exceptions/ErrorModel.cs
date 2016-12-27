namespace Knowlead.Common.Exceptions
{
    public class ErrorModel
    {
        public string Value { get; set; }
        
        private string param;
        public string Param
        {
            get
            {
                return this.param;
            }
            set
            { 
                this.param = ((string.IsNullOrWhiteSpace(value))? value : $":{value}");
            }
        }

        public ErrorModel(string @value, string parameter = null)
        {
            this.Value = @value;
            this.Param = parameter;
        }

        public ErrorModel()
        {
            this.Value = "Default Value";
            this.param = null;
        }

        
    }
}