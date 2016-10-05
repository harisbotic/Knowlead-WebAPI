namespace Knowlead.DTO.ResponseModels
{
    public class FormErrorModel : ErrorModel
    {
        private string key;
        public string Key
        {
            get
            {
                return this.key;
            }
            set
            { 
                this.key = value.ToLower();
            }
        }

        public FormErrorModel(string key, string @value, string parameter = null): base(@value, parameter)
        {
            this.Key = key;
            this.Value = @value;
            this.Param = parameter;
        }

        public FormErrorModel()
        {
            this.Key = "Default Key";
            this.Value = "Default Value";
            this.Param = null;
        }
    }
}