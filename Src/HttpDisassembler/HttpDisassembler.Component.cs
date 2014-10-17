using System;
using System.Collections;

namespace BizTalkComponents.PipelineComponents.HttpDisassembler
{
    public partial class HttpDisassembler
    {
        public string Name { get; private set; }
        public string Version { get; private set; }
        public string Description { get; private set; }
        public void GetClassID(out Guid classID)
        {
            throw new NotImplementedException();
        }

        public void InitNew()
        {
            throw new NotImplementedException();
        }

        public IEnumerator Validate(object projectSystem)
        {
            throw new NotImplementedException();
        }

        public IntPtr Icon { get; private set; }
    }
}
