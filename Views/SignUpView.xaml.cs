using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeasonalBite.ViewModels;

namespace SeasonalBite.Views;

public partial class SignUpView : ContentPage
{
    public SignUpView(SignUpViewModel signUpViewModel)
    {
        InitializeComponent();
        
        BindingContext = signUpViewModel;
    }
}