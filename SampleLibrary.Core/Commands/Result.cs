using System.Collections.Generic;

namespace SampleLibrary.Core.Commands
{
    public class Result
    {
        public IEnumerable<string> Errors { get; set; }
        public bool Success { get; set; }
    }
}