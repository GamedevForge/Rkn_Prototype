using Project.Common.Core;

namespace Project.Common.UI
{
    public class RequirementsWindowViewModel : BaseWindowViewModel 
    { 
        private readonly RequirementsModel _requirementsModel;

        public string Text => _requirementsModel.CurrentConfig.Text;

        public RequirementsWindowViewModel(RequirementsModel requirementsModel) =>
            _requirementsModel = requirementsModel;
    }
}