using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Recognizers.Text;
using Microsoft.Recognizers.Text.DateTime;
using Microsoft.Bot.Schema;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.Extensions.Logging;
using Microsoft.Recognizers.Text.DataTypes.TimexExpression;

namespace saganBot.Dialogs
{
    public class calcularEdad : ComponentDialog
    {
       private readonly string[] _planetas = new string[]
       {
            "Mercurio", "Venus", "Marte",
       };

        private string fechaSelected;
        private const string DestinationStepMsgText = "¿Cual es tu fecha de nacimiento?";


        private const string DoneOption = "Volver";
        public calcularEdad() : base(nameof(calcularEdad))
        {
            

            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
            AddDialog(new ConfirmPrompt(nameof(ConfirmPrompt)));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
               
                IntroStepAsync,
                ActStepAsync,
                fechaConfirmStepAsync,
                loopStepAsync,
                SeleccionStepAsync,
                procesarplanetaStepAsync,
                //FinalStepAsync,
            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> IntroStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivitiesAsync(
          new Activity[] {
                new Activity { Type = ActivityTypes.Typing },
              //new Activity { Type = "delay", Value= 3000 },
          },
          cancellationToken);
            var promptMessage = MessageFactory.Text(DestinationStepMsgText, DestinationStepMsgText, InputHints.ExpectingInput);
            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);
        }

        private async Task<DialogTurnResult> ActStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivitiesAsync(
          new Activity[] {
                new Activity { Type = ActivityTypes.Typing },
              //new Activity { Type = "delay", Value= 3000 },
          },
          cancellationToken);
            var choice = (string)stepContext.Result;
            string message;
            string Date;

            if (ValidateDate(choice, out var date, out message))
            {
                Date = date;
                fechaSelected = date;
                //await stepContext.Context.SendActivityAsync($"Your cab ride to the airport is scheduled for {Date}.");
                //await stepContext.Context.SendActivityAsync($"Type anything to run the bot again.");
            }
            else
            {
                await stepContext.Context.SendActivityAsync(message ?? "Lo siento, no entendi eso :(.", null, null, cancellationToken);
                return await stepContext.ReplaceDialogAsync(nameof(calcularEdad), null, cancellationToken);
            }
            return await stepContext.NextAsync();

        }

        private async Task<DialogTurnResult> fechaConfirmStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivitiesAsync(
          new Activity[] {
                new Activity { Type = ActivityTypes.Typing },
              //new Activity { Type = "delay", Value= 3000 },
          },
          cancellationToken);
            await stepContext.Context.SendActivityAsync(MessageFactory.Text($"Tengo tu fecha de nacimiento como {fechaSelected}."), cancellationToken);
            return await stepContext.PromptAsync(nameof(ConfirmPrompt), new PromptOptions { Prompt = MessageFactory.Text("¿Es correcto?") }, cancellationToken);

        }

        private async Task<DialogTurnResult> loopStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivitiesAsync(
          new Activity[] {
                new Activity { Type = ActivityTypes.Typing },
              //new Activity { Type = "delay", Value= 3000 },
          },
          cancellationToken);
            if ((bool)stepContext.Result)
            {
                return await stepContext.NextAsync();
            }
            else
            {
                return await stepContext.ReplaceDialogAsync(nameof(calcularEdad), null, cancellationToken);
            }
        }
        private static bool ValidateDate(string input, out string date, out string message)
        {
            
            date = null;
            message = null;

            // Try to recognize the input as a date-time. This works for responses such as "11/14/2018", "9pm", "tomorrow", "Sunday at 5pm", and so on.
            // The recognizer returns a list of potential recognition results, if any.
            try
            {
                var results = DateTimeRecognizer.RecognizeDateTime(input, Culture.Spanish);

                // Check whether any of the recognized date-times are appropriate,
                // and if so, return the first appropriate date-time. We're checking for a value at least an hour in the future.
                //var earliest = DateTime.Now.AddHours(1.0);

                foreach (var result in results)
                {
                    // The result resolution is a dictionary, where the "values" entry contains the processed input.
                    var resolutions = result.Resolution["values"] as List<Dictionary<string, string>>;

                    foreach (var resolution in resolutions)
                    {
                        // The processed input contains a "value" entry if it is a date-time value, or "start" and
                        // "end" entries if it is a date-time range.
                        if (resolution.TryGetValue("value", out var dateString)
                            || resolution.TryGetValue("start", out dateString))
                        {
                            if (DateTime.TryParse(dateString, out var candidate))
                            {
                                date = candidate.ToShortDateString();
                                return true;
                            }
                        }
                    }
                }

                message = "Lo siento no puede interpretar eso como una fecha";
            }
            catch
            {
                message = "Lo siento no puede interpretar eso como una fecha.";
            }

            return false;
        }

        private async Task<DialogTurnResult> SeleccionStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivitiesAsync(
          new Activity[] {
                new Activity { Type = ActivityTypes.Typing },
              //new Activity { Type = "delay", Value= 3000 },
          },
          cancellationToken);
            var options = _planetas.ToList();
            options.Add(DoneOption);

            return await stepContext.PromptAsync(nameof(ChoicePrompt),
                  new PromptOptions
                  {
                      Prompt = MessageFactory.Text("Perfecto. ¿En cuál planeta deseas calcular tu edad?. Por el momento tengo los soguientes disponibles"), //Frase inicial de la pregunta
                      RetryPrompt = MessageFactory.Text("Lo siento no entendi. Elige uno de la lista."), //en caso de elegir una pcion inexistente se encia este mensaje
                      Choices = ChoiceFactory.ToChoices(options), //opciones
                  }, cancellationToken);
        }

        private async Task<DialogTurnResult> procesarplanetaStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivitiesAsync(
          new Activity[] {
                new Activity { Type = ActivityTypes.Typing },
              //new Activity { Type = "delay", Value= 3000 },
          },
          cancellationToken);
            var choice = (FoundChoice)stepContext.Result;
            var done = choice.Value == DoneOption;
            DateTime dateTime = DateTime.Now;
            DateTime fecha1 = DateTime.Parse(fechaSelected);
            TimeSpan timeSpan = dateTime - fecha1;
            int dias = timeSpan.Days;
            double edad;

            switch (choice.Value)
            {
                case "Mercurio":
                    edad = dias / 88;
                    await stepContext.Context.SendActivityAsync(MessageFactory.Text("Un año en mercurio son 88 dias"), cancellationToken);
                    await stepContext.Context.SendActivityAsync(MessageFactory.Text($"Han pasado {dias} dias desde tu nacimiento"), cancellationToken);
                    await stepContext.Context.SendActivityAsync(MessageFactory.Text($"Tienes {edad} años mercurianos"), cancellationToken);
                    break;
                case "Venus":
                    edad = dias / 255;
                    await stepContext.Context.SendActivityAsync(MessageFactory.Text("Un año en mercurio son 255 dias"), cancellationToken);
                    await stepContext.Context.SendActivityAsync(MessageFactory.Text($"Han pasado {dias} dias desde tu nacimiento"), cancellationToken);
                    await stepContext.Context.SendActivityAsync(MessageFactory.Text($"Tienes {edad} años en Venus"), cancellationToken);
                    break;
                case "Marte":
                    edad = dias / 687;
                    await stepContext.Context.SendActivityAsync(MessageFactory.Text("Un año en mercurio son 687 dias"), cancellationToken);
                    await stepContext.Context.SendActivityAsync(MessageFactory.Text($"Han pasado {dias} dias desde tu nacimiento"), cancellationToken);
                    await stepContext.Context.SendActivityAsync(MessageFactory.Text($"Tienes {edad} años en Marcianos"), cancellationToken);
                    break;
                default:
                    
                    break;
            }
            await stepContext.Context.SendActivityAsync(MessageFactory.Text("Puedes enviar cualquier mensaje para continuar"), cancellationToken);
            return await stepContext.EndDialogAsync();

        }
    }
}
