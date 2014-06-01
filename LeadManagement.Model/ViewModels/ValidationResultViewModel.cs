using System;
using System.Collections.Generic;

namespace LeadManagement.Model.ViewModels
{
    public class ValidationResultViewModel
    {
        private readonly Lazy<List<string>> _errors = new Lazy<List<string>>();
        
        public bool Success { get; set; }
        public IList<string> Errors { get { return _errors.Value; } }
    }
}
