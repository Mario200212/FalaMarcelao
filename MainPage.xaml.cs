using Microsoft.Maui.Controls; // Para controles MAUI
using Microsoft.Maui;           // Para funcionalidades gerais do MAUI
using System.Text.RegularExpressions; // Para expressões regulares
using Microsoft.Maui.Controls.Compatibility;
using System.Speech.Synthesis;
namespace FalaMarcelãov3
{
    public partial class MainPage : ContentPage
    {
        public string[] words;
        public string Texto;
        public int indicePalavra;
        private CancellationTokenSource _cts;

        private SpeechSynthesizer _synth;
        private string _currentLocale = "pt-BR"; // Padrão para português do Brasil

        public MainPage()
        {
            InitializeComponent();
            _synth = new SpeechSynthesizer();
            SetSpeechLanguage(_currentLocale);  
        }

        public void SetSpeechLanguage(string culture)
        {
            var cultureInfo = new System.Globalization.CultureInfo(culture);
            _synth.SelectVoiceByHints(VoiceGender.NotSet, VoiceAge.NotSet, 0, cultureInfo);
        }

        public void SpeakSettings()
        {
            _synth.SpeakAsyncCancelAll();
            int speed = (int)SpeedSlider.Value;
            _synth.Rate = speed;
            string texto = EditorText.Text;
            if (!string.IsNullOrEmpty(texto))
            {
                _synth.SpeakAsync(texto);
            }
        }

        private bool IsVoiceAvailable(string locale)
        {
            return _synth.GetInstalledVoices()
                          .Any(voice => voice.VoiceInfo.Culture.Name.Equals(locale, StringComparison.OrdinalIgnoreCase));
        }
        private async void OnSpeakButtonClicked(object sender, EventArgs e)
        {
            if (IsVoiceAvailable(_currentLocale))
            {
                SpeakSettings();
            }
            else
            {
                await DisplayAlert("Voz não encontrada", "A voz em português do Brasil não está instalada em seu sistema Windows. Você será redirecionado para um tutorial de instalação.", "OK");
                await Browser.OpenAsync("https://support.microsoft.com/pt-br/topic/baixar-idiomas-e-vozes-para-leitura-avan%C3%A7ada-modo-de-leitura-e-leitura-em-voz-alta-4c83a8d8-7486-42f7-8e46-2b0fdf753130#:~:text=Abra%20o%20Painel%20de%20Controle.%20Selecione%20o%20Idioma.,selecione%20Baixar%20e%20instalar%20o%20pacote%20de%20idiomas.", BrowserLaunchMode.SystemPreferred);
                Environment.Exit(0);
            }
        }

        public async void BotaoPlayPalavraAtual(object sender, EventArgs e)
        {
            if (IsVoiceAvailable(_currentLocale))
            {
                if (words != null)
                {
                    string word = words[indicePalavra];
                    _synth.SpeakAsyncCancelAll();
                    _synth.Rate = (int)SpeedSlider.Value;
                    if (word != null)
                    {
                        _synth.SpeakAsync(word);
                    }
                }
            }
            else
            {
                await DisplayAlert("Voz não encontrada", "A voz em português do Brasil não está instalada em seu sistema Windows. Você será redirecionado para um tutorial de instalação.", "OK");
                await Browser.OpenAsync("https://support.microsoft.com/pt-br/topic/baixar-idiomas-e-vozes-para-leitura-avan%C3%A7ada-modo-de-leitura-e-leitura-em-voz-alta-4c83a8d8-7486-42f7-8e46-2b0fdf753130#:~:text=Abra%20o%20Painel%20de%20Controle.%20Selecione%20o%20Idioma.,selecione%20Baixar%20e%20instalar%20o%20pacote%20de%20idiomas.", BrowserLaunchMode.SystemPreferred);
                Environment.Exit(0);
            }

        }

        public async void BotaoProximaPalavra(object sender, EventArgs e)
        {
            if (IsVoiceAvailable(_currentLocale))
            {
                if (words != null)
                {
                    if (words.Length > 1 && indicePalavra != words.Length - 1)
                    {
                        if (indicePalavra < words.Length - 1)
                        {
                            indicePalavra++;
                            RemoveAllItems();
                            PercorrePalavra_i(indicePalavra);
                        }
                    }
                }
            }
            else
            {
                await DisplayAlert("Voz não encontrada", "A voz em português do Brasil não está instalada em seu sistema Windows. Você será redirecionado para um tutorial de instalação.", "OK");
                await Browser.OpenAsync("https://support.microsoft.com/pt-br/topic/baixar-idiomas-e-vozes-para-leitura-avan%C3%A7ada-modo-de-leitura-e-leitura-em-voz-alta-4c83a8d8-7486-42f7-8e46-2b0fdf753130#:~:text=Abra%20o%20Painel%20de%20Controle.%20Selecione%20o%20Idioma.,selecione%20Baixar%20e%20instalar%20o%20pacote%20de%20idiomas.", BrowserLaunchMode.SystemPreferred);
                Environment.Exit(0);
            }
            
        }

        public async void BotaoPalavraAnterior(object sender, EventArgs e)
        {
            if (IsVoiceAvailable(_currentLocale))
            {
                if (words != null)
                {
                    if (words.Length > 1 && indicePalavra != 0)
                    {
                        if (indicePalavra > 0)
                        {
                            indicePalavra--;
                            RemoveAllItems();
                            PercorrePalavra_i(indicePalavra);
                        }
                    }
                }
            }
            else
            {
                await DisplayAlert("Voz não encontrada", "A voz em português do Brasil não está instalada em seu sistema Windows. Você será redirecionado para um tutorial de instalação.", "OK");
                await Browser.OpenAsync("https://support.microsoft.com/pt-br/topic/baixar-idiomas-e-vozes-para-leitura-avan%C3%A7ada-modo-de-leitura-e-leitura-em-voz-alta-4c83a8d8-7486-42f7-8e46-2b0fdf753130#:~:text=Abra%20o%20Painel%20de%20Controle.%20Selecione%20o%20Idioma.,selecione%20Baixar%20e%20instalar%20o%20pacote%20de%20idiomas.", BrowserLaunchMode.SystemPreferred);
                Environment.Exit(0);
            }
            
        }

        public async void BotaoDividePalavras(object sender, EventArgs e)
        {

            if (IsVoiceAvailable(_currentLocale))
            {
                Divide();
            }
            else
            {
                await DisplayAlert("Voz não encontrada", "A voz em português do Brasil não está instalada em seu sistema Windows. Você será redirecionado para um tutorial de instalação.", "OK");
                await Browser.OpenAsync("https://support.microsoft.com/pt-br/topic/baixar-idiomas-e-vozes-para-leitura-avan%C3%A7ada-modo-de-leitura-e-leitura-em-voz-alta-4c83a8d8-7486-42f7-8e46-2b0fdf753130#:~:text=Abra%20o%20Painel%20de%20Controle.%20Selecione%20o%20Idioma.,selecione%20Baixar%20e%20instalar%20o%20pacote%20de%20idiomas.", BrowserLaunchMode.SystemPreferred);
                Environment.Exit(0);
            }
           
        }

        public void Divide()
        {
            indicePalavra = 0;
            Texto = EditorText.Text;
            DivideString(Texto);

            if (words != null)
            {
                if (words[indicePalavra] != "")
                {
                    RemoveAllItems();
                    PercorrePalavra_i(0);
                }
                else
                {
                    RemoveAllItems();
                }
            }
        }

        private void AddImage(string imagePath)
        {
            var image = new Image
            {
                Source = imagePath,
                HeightRequest = 250,
                Margin = new Thickness(5)
            };

            ImagesContainer.Children.Add(image);
        }

        public string LimparString(string texto)
        {
            if (texto != null)
            {
                texto = Regex.Replace(texto, @"[^a-zA-ZáéíóúÁÉÍÓÚâêîôÂÊÎÔãõÃÕàèìòùÀÈÌÒÙçÇ\s]", " ");
                texto = Regex.Replace(texto, @"\s+", " ");
            }
            return texto;
        }

        void DivideString(string input)
        {
            if (input != null)
            {
                input = LimparString(input);
                words = input.Split(' ');
            }
        }

        private void RemoveAllItems()
        {
            ImagesContainer.Children.Clear();
        }

        public void PercorrePalavra_i(int i)
        {
            if (i < 0 || i >= words.Length)
            {
                Console.WriteLine("Índice fora do intervalo do array de palavras.");
                return;
            }

            string word = words[i];
            word = word.ToUpper();
            for (int j = 0; j < word.Length; j++)
            {
                VerificaLetra(word, j);
            }
        }




        private void VerificaLetra(string word, int j)
        {
            if (word[j] == 'A' || word[j] == 'Á' || word[j] == 'À' || word[j] == 'Â' || word[j] == 'Ã')
            {
                if (word[j] == 'A')
                    AddImage("a.png");

                if (word[j] == 'À')
                    AddImage("acr.png");

                if (word[j] == 'Á')
                    AddImage("aagud.png");

                if (word[j] == 'Â')
                    AddImage("acir.png");

                if (word[j] == 'Ã')
                    AddImage("atil.png");
            }

            if (word[j] == 'E' || word[j] == 'É' || word[j] == 'È' || word[j] == 'Ê')
            {
                if (word[j] == 'E')
                    AddImage("e.png");

                if (word[j] == 'É')
                    AddImage("eagud.png");
                
                if (word[j] == 'È')
                    AddImage("e.png");

                if (word[j] == 'Ê')
                    AddImage("ecir.png");
            }
            if (word[j] == 'I' || word[j] == 'Í' || word[j] == 'Ì' || word[j] == 'Î')
            {
                if (word[j] == 'I')
                    AddImage("i.png");

                if (word[j] == 'Í')
                    AddImage("iagud.png");

                if (word[j] == 'Ì')
                    AddImage("icr.png");

                if (word[j] == 'Î')
                    AddImage("icir.png");
            }

            if (word[j] == 'O' || word[j] == 'Ó' || word[j] == 'Ò' || word[j] == 'Ô' || word[j] == 'Õ')
            {
                if (word[j] == 'O')
                    AddImage("o.png");

                if (word[j] == 'Ó')
                    AddImage("oagud.png");

                if (word[j] == 'Ò')
                    AddImage("ocr.png");

                if (word[j] == 'Ô')
                    AddImage("ocir.png");

                if (word[j] == 'Õ')
                    AddImage("otil.png");
            }

            if (word[j] == 'U' || word[j] == 'Ú' || word[j] == 'Ù' || word[j] == 'Û')
            {
                if (j > 0)
                {
                    if (word[j - 1] != 'Q')
                    {
                        if (word[j] == 'U')
                            AddImage("u.png");

                        if (word[j] == 'Ú')
                            AddImage("uagud.png");

                        if (word[j] == 'Ù')
                            AddImage("ucr.png");

                        if (word[j] == 'Û')
                            AddImage("ucir.png");
                    }
                }
                else
                {
                    if (word[j] == 'U')
                        AddImage("u.png");

                    if (word[j] == 'Ú')
                        AddImage("uagud.png");

                    if (word[j] == 'Ù')
                        AddImage("ucr.png");

                    if (word[j] == 'Û')
                        AddImage("ucir.png");
                }

            }
            if (word[j] == 'B')
            {
                AddImage("b.png");
            }

            if (word[j] == 'C')
            {
                if (word.Length - 1 > j)
                {
                    if (word[j + 1] == 'A' || word[j + 1] == 'Á' || word[j + 1] == 'À' || word[j + 1] == 'Â' || word[j + 1] == 'Ã' ||
                        word[j + 1] == 'O' || word[j + 1] == 'Ó' || word[j + 1] == 'Ò' || word[j + 1] == 'Ô' || word[j + 1] == 'Õ' ||
                        word[j + 1] == 'U' || word[j + 1] == 'Ú' || word[j + 1] == 'Ù' || word[j + 1] == 'Û')
                    {
                        AddImage("cacocu.png");
                    }
                    else
                    {
                        if (word[j + 1] == 'E' || word[j + 1] == 'É' || word[j + 1] == 'È' || word[j + 1] == 'Ê' ||
                           word[j + 1] == 'I' || word[j + 1] == 'Í' || word[j + 1] == 'Ì' || word[j + 1] == 'Î')
                        {
                            AddImage("ceci.png");
                        }
                        else // Se C estiver no meio da palavra e não possuir uma vogal depois
                        {
                            if (word[j + 1] == 'H') // Se a consoante dps do C for um H, bote som de CH
                            {
                                AddImage("ch.png");
                            }
                            else //Se C estiver no meio da palavra e não possuir uma vogal  ou H depois, bote o som de Ce, ci.
                            {
                                if (word[j + 1] == 'R')
                                {
                                    AddImage("cacocu.png");
                                }
                                else
                                {
                                    if (word[j + 1] == 'L')
                                    {
                                        AddImage("cacocu.png");
                                    }
                                    else
                                    {
                                        AddImage("cacocu.png");
                                    }
                                }


                            }

                        }
                    }
                }
                else // Se o C estiver na última posição, vou convencionar que apareça o CECI
                {// Em Português esse caso não existe
                    AddImage("ceci.png");
                }
            }
            if (word[j] == 'D')
            {
                AddImage("d.png");
            }

            if (word[j] == 'F')
            {
                AddImage("f.png");
            }
            if (word[j] == 'G')
            {
                if (word.Length - 1 > j)
                {
                    if (word[j + 1] == 'A' || word[j + 1] == 'Á' || word[j + 1] == 'À' || word[j + 1] == 'Â' || word[j + 1] == 'Ã' ||
                        word[j + 1] == 'O' || word[j + 1] == 'Ó' || word[j + 1] == 'Ò' || word[j + 1] == 'Ô' || word[j + 1] == 'Õ' ||
                        word[j + 1] == 'U' || word[j + 1] == 'Ú' || word[j + 1] == 'Ù' || word[j + 1] == 'Û')
                    {
                        AddImage("gagogu.png");
                    }
                    else
                    {
                        if (word[j + 1] == 'E' || word[j + 1] == 'É' || word[j + 1] == 'È' || word[j + 1] == 'Ê' ||
                           word[j + 1] == 'I' || word[j + 1] == 'Í' || word[j + 1] == 'Ì' || word[j + 1] == 'Î')
                        {
                            AddImage("gegi.png");
                        }
                        else // Se G estiver no meio e não possui uma vogal dps, bote o som de Ge, Gi
                        {
                            if (word[j + 1] == 'R')
                            {
                                AddImage("gagogu.png");
                            }
                            else
                            {
                                if (word[j + 1] == 'L')
                                {
                                    AddImage("gagogu.png");
                                }
                                else
                                {
                                    AddImage("gagogu.png");
                                }
                            }


                        }
                    }
                }
                else // Se o G estiver na última posição, vou convencionar que apareça o GEGI
                {
                    AddImage("gegi.png");
                }
            }
            if (word[j] == 'H')
            {
                int Comp = word.Length;
                if (j == 0 || j==Comp-1)
                {
                    if(j==0)
                    {
                        AddImage("h.png");
                    }
                    if(j==Comp-1) // Se o H aparecer na última posição
                    {
                        AddImage("hultima.png");
                    }
                }
                else
                {
                    if (word[j - 1] != 'L' && word[j - 1] != 'N' && word[j - 1] != 'C')
                    {
                        AddImage("h.png");
                    }
                }
            }

            if (word[j] == 'J')
            {
                AddImage("j.png");
            }

            if (word[j] == 'K')
            {
                AddImage("k.png");
            }

            if (word[j] == 'L')
            {
                if (word.Length - 1 > j)
                {
                    if (word[j + 1] == 'H')
                    {
                        AddImage("lh.png");
                    }
                    else
                    {

                        AddImage("l.png");

                    }
                }
                else //// Se o L estiver na última posicao
                {//Tirar uma nova foto desse caso
                    AddImage("l.png");
                }
            }

            if (word[j] == 'M')
            {
                AddImage("m.png");
            }

            if (word[j] == 'N')
            {
                if (word.Length - 1 > j)
                {
                    if (word[j + 1] == 'H')
                    {
                        AddImage("nh.png");
                    }
                    else
                    {
                        AddImage("n.png");
                    }
                }
                else //// Se o N estiver na última posicao
                {
                    AddImage("n.png");
                }
            }

            if (word[j] == 'P')
            {
                AddImage("p.png");
            }
            if (word[j] == 'Q')
            {
                AddImage("qu.png");
            }
            if (word[j] == 'R')
            {
                if (j == 0) // Se o R estiver na 1ª posição, o som é de rr. Ex.: Rosa
                {
                    AddImage("ara.png");
                }
                else
                {
                    if (word.Length - 1 > j)
                    {
                        if (word[j + 1] == 'R') // Se tiver rr, o som vai ser de rr. Ex.: Carroça
                        {
                            AddImage("rr.png");
                        }
                        else // Se o R estiver sozinho no meio da palavra o som é tipo ARA(Lingua treme). Ex.: Cratera
                        {
                            if (word[j - 1] != 'R')
                            {
                                AddImage("ara.png");
                            }
                        }
                    }
                    else // Se R estiver na ultima posição da palavra o som é de ARA. Ex.: Bater (Lingua treme no final)
                    {
                        AddImage("ara.png");
                    }
                }

            }
            if (word[j] == 'S')
            {
                if (j == 0) // Se S estiver na primeira posição, o som é de ça,çe,...,çu. Ex.: sapo
                {
                    AddImage("s.png");
                }
                else
                {
                    if (word.Length - 1 > j) // Se o S estiver no meio da palavra
                    {
                        if (word[j + 1] != 'A' || word[j + 1] != 'Á' || word[j + 1] != 'À' || word[j + 1] != 'Â' || word[j + 1] != 'Ã' ||
                        word[j + 1] != 'O' || word[j + 1] != 'Ó' || word[j + 1] != 'Ò' || word[j + 1] != 'Ô' || word[j + 1] != 'Õ' ||
                        word[j + 1] != 'U' || word[j + 1] != 'Ú' || word[j + 1] != 'Ù' || word[j + 1] != 'Û')
                        {// Se a próxima letra não for uma vogal, teremos som de ççççççç. Ex.: Cascata
                            AddImage("s.png");
                        }
                        else // Se a próxima letra for uma vogal, teremos som de z. Ex.: Casa
                        {
                            AddImage("asa.png");
                        }
                    }
                    else // Se S estiver na última posição do palavra, teremos som de çççççççç. Ex.: Meninos
                    {
                        AddImage("s.png");
                    }
                }
            }
            if (word[j] == 'T')
            {
                AddImage("t.png");
            }

            if (word[j] == 'V')
            {
                AddImage("v.png");
            }
            //if (word[j] == 'W')
            //{
            //    AddImage(itemPrefab[22]);
            //}

            if (word[j] == 'X')
            {
                AddImage("x.png");
            }
            //if (word[j] == 'Y')
            //{
            //  AddImage(itemPrefab[24]);
            //}
            if (word[j] == 'Z')
            {
                AddImage("z.png");
            }
            if (word[j] == 'Ç')
            {
                AddImage("ccidilha.png");
            }

        }
    }
}


