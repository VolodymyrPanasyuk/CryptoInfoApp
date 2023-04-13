using CryptoInfoApp.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoInfoApp.ViewModel;
public class MainViewModel : ObservableObject
{
    private Dictionary<Type, ObservableObject> _viewModelsCache;
    private ObservableObject _currentView;
    private OnlyForReloadViewModel _onlyForReloadViewModel;

    public ObservableObject CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged(nameof(CurrentView));
        }
    }

    public RelayCommand HomeViewCommand { get; }
    public RelayCommand CoinViewCommand { get; }
    public RelayCommand<string> CoinViewWithIdCommand { get; }
    public RelayCommand SearchViewCommand { get; }
    public RelayCommand<string> SearchViewWithInputCommand { get; }
    public RelayCommand ConvertViewCommand { get; }
    public RelayCommand ReloadCurrentViewCommand { get; }

    public MainViewModel()
    {
        _viewModelsCache = new Dictionary<Type, ObservableObject>();
        _onlyForReloadViewModel = new OnlyForReloadViewModel();

        HomeViewCommand = new RelayCommand(x => SetCurrentViewModel<HomeViewModel>());
        CoinViewCommand = new RelayCommand(x => SetCurrentViewModel<CoinViewModel>());
        CoinViewWithIdCommand = new RelayCommand<string>(coinId => SetCoinViewModelWithId(coinId));
        SearchViewCommand = new RelayCommand(x => SetCurrentViewModel<SearchViewModel>());
        SearchViewWithInputCommand = new RelayCommand<string>(userInput => SetSearchViewModelWithUserInput(userInput));
        ConvertViewCommand = new RelayCommand(x => SetCurrentViewModel<ConvertViewModel>());
        ReloadCurrentViewCommand = new RelayCommand(x => ReloadCurrentViewModel());

        SetCurrentViewModel<HomeViewModel>();
    }

    private void SetCurrentViewModel<T>() where T : ObservableObject
    {
        Type viewModelType = typeof(T);

        if (_viewModelsCache.ContainsKey(viewModelType))
        {
            CurrentView = _viewModelsCache[viewModelType];
        }
        else
        {
            ObservableObject viewModel = Activator.CreateInstance<T>();
            _viewModelsCache.Add(viewModelType, viewModel);
            CurrentView = viewModel;
        }
    }

    private void SetCoinViewModelWithId(string coinId)
    {
        CoinViewModel coinViewModel = new CoinViewModel(coinId);
        if (_viewModelsCache.ContainsKey(typeof(CoinViewModel)))
        {
            _viewModelsCache[typeof(CoinViewModel)] = coinViewModel;
        }
        else
        {
            _viewModelsCache.Add(typeof(CoinViewModel), coinViewModel);
        }
        CurrentView = coinViewModel;
    }

    private async void SetSearchViewModelWithUserInput(string userInput)
    {
        SearchViewModel searchViewModel = new SearchViewModel(userInput);
        if (_viewModelsCache.ContainsKey(typeof(SearchViewModel)))
        {
            CurrentView = _onlyForReloadViewModel;
            await Task.Delay(1);

            _viewModelsCache[typeof(SearchViewModel)] = searchViewModel;
        }
        else
        {
            _viewModelsCache.Add(typeof(SearchViewModel), searchViewModel);
        }
        CurrentView = searchViewModel;
    }

    private async void ReloadCurrentViewModel()
    {
        if (CurrentView is ObservableObject currentViewModel)
        {
            CurrentView = _onlyForReloadViewModel;
            await Task.Delay(1);

            Type viewModelType = currentViewModel.GetType();
            var newViewModel = CreateNewInstance(viewModelType);

            if (newViewModel != null)
            {
                _viewModelsCache[viewModelType] = newViewModel;
                CurrentView = newViewModel;
            }
        }
    }

    private ObservableObject CreateNewInstance(Type viewModelType) => (ObservableObject)Activator.CreateInstance(viewModelType);
}