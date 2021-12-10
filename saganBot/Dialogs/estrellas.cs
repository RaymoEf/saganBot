using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;

namespace saganBot.Dialogs
{
    public class estrellas : ComponentDialog
    {
        private const string DoneOption = "Cancelar";

        private const string Selected = "value-companiesSelected";

        private readonly string[] _estrellas = new string[]
        {
            "Sol", "Alpha Centauri A", "Alpha Centauri B", "Proxima Centauri", "Betelgeuse",
        };

        public estrellas(): base(nameof(estrellas))
        {
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt))); //El Dialogo de las opciones
            AddDialog(new calcularEdad());
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[] //El dialogo en cascada con los dialogos a mostrar
            {
                ChoiceCardStepAsync,
                ShowCardStepAsync,
            }));

            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> ChoiceCardStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivitiesAsync(
          new Activity[] {
                new Activity { Type = ActivityTypes.Typing },
              //new Activity { Type = "delay", Value= 3000 },
          },
          cancellationToken);
            var list = stepContext.Options as List<string> ?? new List<string>();
            stepContext.Values[Selected] = list;
            string message;
            var options = _estrellas.ToList();

            if (list.Count is 0)
            {
                await stepContext.Context.SendActivityAsync(MessageFactory.Text("Estrellas. Gran eleccion."), cancellationToken);
                await stepContext.Context.SendActivityAsync(MessageFactory.Text("Sabias que?."), cancellationToken);
                await stepContext.Context.SendActivityAsync(MessageFactory.Text("Las estrellas son enormes 'esferas' de plasma que fusionan un elemento en otro mas pesado."), cancellationToken);
                await stepContext.Context.SendActivityAsync(MessageFactory.Text("El elemento que se fusiona depende de la masa y la composicion de la estrella."), cancellationToken);
                await stepContext.Context.SendActivityAsync(MessageFactory.Text("Esta fusion nuclear evita que la estrella colapse en si misma debido a la gravedad"), cancellationToken);
                message = $"Selecciona la estrela de la que quieras saber algo, o `{DoneOption}` para finalizar.";
                options.Add(DoneOption);
            }
            else
            {
                message = $"Puedes elegir otra, " + $"o `{DoneOption}` para finalizar.";
                options.Add("Calculadora de Edad");
                options.Add(DoneOption);
            }

            if (list.Count > 0)
            {
                int conteo = list.Count
                    , i = 0;
                do
                {
                    options.Remove(list[i]);
                    i++;
                } while (i < conteo);
                //options.Remove(list[conteo-1]);
            }

            //Creamos el mensaje con las opciones dadas a el usuario
            var promptOptions = new PromptOptions
            {
                Prompt = MessageFactory.Text(message),
                RetryPrompt = MessageFactory.Text("No entendí eso, por favor elige uno de la lista."),
                Choices = ChoiceFactory.ToChoices(options),
            };

            // Prompt the user with the configured PromptOptions.
            return await stepContext.PromptAsync(nameof(ChoicePrompt), promptOptions, cancellationToken);            //envia el mensaje al usuario, y se manda a llamar el siguiente paso
        }

        private async Task<DialogTurnResult> ShowCardStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivitiesAsync(
            new Activity[] {
                new Activity { Type = ActivityTypes.Typing },
                //new Activity { Type = "delay", Value= 3000 },
            },
            cancellationToken);
            var list = stepContext.Values[Selected] as List<string>;
            var choice = (FoundChoice)stepContext.Result;
            var done = choice.Value == DoneOption;
            var attachments = new List<Attachment>(); //la variable en laque se guardaan las tarjetas para despues mostrarse

            // Reply to the activity we received with an activity.
            var reply = MessageFactory.Attachment(attachments); //la variable repl, que sera la encargada de mostrat los mensajes

            // Segun la opcion elegida, se despliegan las tarjetas corresponientes
            await stepContext.Context.SendActivitiesAsync(
          new Activity[] {
                new Activity { Type = ActivityTypes.Typing },
              //new Activity { Type = "delay", Value= 3000 },
          },
          cancellationToken);
            switch (choice.Value)
            {
                case "Sol":
                    reply.Attachments.Add(Cards.Cards.solCard());

                    break;
                case "Alpha Centauri A":
                    //reply.Attachments.Add(Cards.Cards)
                    reply.Attachments.Add(Cards.Cards.centauriACard());
                    break;
                case "Alpha Centauri B":
                    reply.Attachments.Add(Cards.Cards.centauriBCard());
                    break;
                case "Proxima Centauri":
                    reply.Attachments.Add(Cards.Cards.proximaCard());
                    break;
                case "Betelgeuse":
                    reply.Attachments.Add(Cards.Cards.betelgeuseCard());
                    break;
                default:
                    //Regresar al dialogo principal en caso de seleccionar la opcion de volver
                    break;
            }

            await stepContext.Context.SendActivitiesAsync(
          new Activity[] {
                new Activity { Type = ActivityTypes.Typing },
              //new Activity { Type = "delay", Value= 3000 },
          },
          cancellationToken);
            await stepContext.Context.SendActivityAsync(reply, cancellationToken); //se muestra el mensaje

            if (!done)
            {
                // If they chose a company, add it to the list.;
                list.Add(choice.Value);
            }

            if (done)
            {
                // If they're done, exit and return their list.
                await stepContext.Context.SendActivitiesAsync(
                new Activity[] {
                new Activity { Type = ActivityTypes.Typing },
              //new Activity { Type = "delay", Value= 3000 },
          },
          cancellationToken);
                await stepContext.Context.SendActivityAsync(MessageFactory.Text("Volviendo."), cancellationToken);
                await stepContext.Context.SendActivityAsync(MessageFactory.Text("Envia un mensaje para continuar."), cancellationToken);
                return await stepContext.EndDialogAsync(list, cancellationToken);
            }
            else
            {
                // Otherwise, repeat this dialog, passing in the list from this iteration.
                return await stepContext.ReplaceDialogAsync(nameof(estrellas), list, cancellationToken);
            }
            //return await stepContext.NextAsync(userProfile.PlanetSelected, cancellationToken);

            //return await stepContext.EndDialogAsync(); //fin del dialogo se regresa al dialogo principal
        }
    }
}
