using System;
using System.Windows.Input;
using AppRpgEtec.Models;
using AppRpgEtec.Services.Usuarios;
using AppRpgEtec.ViewModels;


public class UsuarioViewModel : BaseViewModel
{
	private UsuarioService uService;
	public ICommand AutenticarCommand { get; set; }

	#region AtributosPropriedades
	//As propriedades serão chamadas na View fururamente

	private string login = string.Empty;
	public string Login
	{
		get { return login; }
		set
		{
			login = value;
			OnPropertyChanged();
		}
	}

	private string senha = string.Empty;
	public string Senha
	{
		get { return senha; }
		set
		{
			senha = value;
			OnPropertyChanged();
		}
	}
#endregion

	public async Task AutenticarUsuario()//Método para autenticar um usuário
	{
		try
		{
			Usuario u = new Usuario();
			u.Username = Login;
			u.PasswordString = Senha;

			Usuario uAutenticado = await uService.PostAutenticarUsuarioAsync(u);
		}
		catch (Exception ex)
		{
			await Application.Current.MainPage
				.DisplayAlert("Infromação", ex.Message + " Detalhes: " + ex.InnerException, "OK");
		}
	}
}
