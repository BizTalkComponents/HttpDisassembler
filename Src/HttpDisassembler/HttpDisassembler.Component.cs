using System;
using System.Collections;
using System.Linq;
using BizTalkComponents.Utils;

namespace BizTalkComponents.PipelineComponents.HttpDisassembler
{
    public partial class HttpDisassembler
    {
        public string Name { get { return "HttpDisassembler"; } }
        public string Version { get { return "1.0"; } }
        public string Description
        {
            get
            {
                return
                    "Creates message body from URI properties.";
            }
        }
        public void GetClassID(out Guid classID)
        {
            classID = new Guid("BF843732-8C30-4E26-BE35-293C763C3EA1");
        }

        public void InitNew()
        {
            
        }

        public IEnumerator Validate(object projectSystem)
        {
            return ValidationHelper.Validate(this, false).ToArray().GetEnumerator();
        }

        public bool Validate(out string errorMessage)
        {
            var errors = ValidationHelper.Validate(this, true).ToArray();

            if (errors.Any())
            {
                errorMessage = string.Join(",", errors);

                return false;
            }

            errorMessage = string.Empty;

            return true;
        }

        public IntPtr Icon { get { return IntPtr.Zero; } }
    }
}
