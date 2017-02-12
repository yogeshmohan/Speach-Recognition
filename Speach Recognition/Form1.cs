using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Diagnostics;

namespace Speach_Recognition
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        Grammar grammer;
        GrammarBuilder gBuilder;
         public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Choices commands = new Choices();
            commands.Add(new String[] {"notnull","Jarvis"});
            gBuilder = new GrammarBuilder();
            gBuilder.Append(commands);
            grammer = new Grammar(gBuilder);
            recEngine.LoadGrammarAsync(grammer);

            recEngine.SetInputToDefaultAudioDevice();
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            recEngine.SpeechRecognized += recEngine_SpeechRecognized;
        }

        void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Text == "Jarvis")
            {
                Choices commands = new Choices();
                commands.Add(new String[] { "notepad", "mycomputer", "music", "Speak"});
                gBuilder = new GrammarBuilder();
                gBuilder.Append(commands);
                grammer = new Grammar(gBuilder);
                recEngine.LoadGrammarAsync(grammer);
                synthesizer.SpeakAsync("Yes Sir");
            }
            switch (e.Result.Text)
            {
                case "notepad":
                    Process.Start("notepad");
                    break;

                case "mycomputer":
                    Process.Start("explorer");
                    break;

                case "music":
                    WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
                    wplayer.URL = @"C:\\Users\\Yogesh\\Downloads\\Music\\Main Koi Aisa Geet Gaoon(MyMp3Song).mp3";
                    wplayer.controls.play();
                    break;

                case "Speak":
                    PromptBuilder builder = new PromptBuilder();
                    //builder.StartStyle(new PromptStyle());
                   // builder.StartVoice(VoiceGender.Male, VoiceAge.Adult, 2);

                    builder.StartSentence();
                    builder.AppendText("Hello Yogesh");
                    builder.EndSentence();

                    builder.AppendBreak(PromptBreak.None);

                    builder.StartSentence();
                    builder.AppendText("How are You",PromptEmphasis.Strong);
                    builder.EndSentence();


                    synthesizer.SpeakAsync(builder);
                    break;
            }
        }
    }
}
