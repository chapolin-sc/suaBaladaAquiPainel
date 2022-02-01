namespace suaBaladaAqui2.ViewsModels;

public class LoginViewModel
{
    public LoginViewModel(){}
    
    public LoginViewModel(string login, string senha)
    {
        this.login = login;
        this.senha = senha;
    }

    public string login { get; set; }
    public string senha { get; set; }

}