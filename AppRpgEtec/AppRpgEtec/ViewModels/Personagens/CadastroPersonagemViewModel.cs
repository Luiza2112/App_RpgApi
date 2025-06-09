using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AppRpgEtec.Models;
using AppRpgEtec.Models.Enuns;
using AppRpgEtec.Services.Personagens;

namespace AppRpgEtec.ViewModels.Personagens
{
    public class CadastroPersonagemViewModel : BaseViewModel
    {
        private PersonagemService pService;
        public ICommand SalvarCommand { get; }

        public CadastroPersonagemViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            pService = new PersonagemService(token);
            _ = ObterClasses();

        }

        public int id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        public string nome {
            get => nome;
            set
            {
                nome = value;
                OnPropertyChanged();
            }
                }

        public int pontosVida {
            get => pontosVida;
            set
            {
                pontosVida = value;
                OnPropertyChanged();
            }
        }

        public int forca
        {
            get => forca;
            set
            {
                forca = value;
                OnPropertyChanged();
            }
        }

        public int defesa
        {
            get => defesa;
            set
            {
                defesa = value;
                OnPropertyChanged();
            }
        }

        public int inteligencia
        {
            get => inteligencia;
            set
            {
                inteligencia = value;
                OnPropertyChanged();
            }
        }

        public int disputas
        {
            get => disputas;
            set
            {
                disputas = value;
                OnPropertyChanged();
            }
        }

        public int vitorias
        {
            get => vitorias;
            set
            {
                vitorias = value;
                OnPropertyChanged();
            }
        }

        public int derrotas
        {
            get => derrotas;
            set
            {
                derrotas = value;
                OnPropertyChanged();
            }
        }

        //private ObservableCollection<TipoClasse> listaTiposClasse;

        public ObservableCollection<TipoClasse> listaTiposClasse
        {
            get { return listaTiposClasse; }
            set { 
                if(value != null)
                {
                    listaTiposClasse = value;
                    OnPropertyChanged();
                }
            }
        }

        public async Task ObterClasses()
        {
            try
            {
                listaTiposClasse = new ObservableCollection<TipoClasse>();
                listaTiposClasse.Add(new TipoClasse() { Id = 1, Descricao = "Cavaleiro" });
                listaTiposClasse.Add(new TipoClasse() { Id = 2, Descricao = "Mago" });
                listaTiposClasse.Add(new TipoClasse() { Id = 3, Descricao = "Clerigo" });
                OnPropertyChanged(nameof(listaTiposClasse));
            }
            catch (Exception ex) {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public TipoClasse tipoClasseSelecionado;
        public TipoClasse TipoClaseSelecionado
        {
            get { return tipoClasseSelecionado; }
            set {
                if(value != null)
                {
                    tipoClasseSelecionado = value;
                    OnPropertyChanged();
                }
            }
        }

        public async Task SalvarPersonagem()
        {
            try
            {
                Personagem model = new Personagem()
                {
                    Nome = this.nome,
                    PontosVida = this.pontosVida,
                    Defesa = this.defesa,
                    Derrotas = this.derrotas,
                    Disputas = this.disputas,
                    Forca = this.forca,
                    Inteligencia = this.inteligencia,
                    Vitorias = this.vitorias,
                    Id = this.id,
                    Classe = (ClasseEnum)tipoClasseSelecionado.Id
                };
                if (model.Id == 0)
                {
                    await pService.PostPersonagemAsync(model);

                    await Application.Current.MainPage
                        .DisplayAlert("Mensagem", "Dados salvos com sucesso!", "Ok");

                    await Shell.Current.GoToAsync(".."); // Remove a página atual da pilha de páginas
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

    }
}
