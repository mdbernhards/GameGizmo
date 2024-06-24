using CommunityToolkit.Mvvm.ComponentModel;
using GameGizmo.Logic.Interfaces;

namespace GameGizmo.MVVM.ViewModel
{
    internal class HomeViewModel(IApiLogic apiLogic, IApiToViewMapper apiToViewMapper) : ObservableObject
    {
        public IApiLogic ApiLogic { get; set; } = apiLogic;

        public IApiToViewMapper ApiToViewMapper { get; set; } = apiToViewMapper;
    }
}
