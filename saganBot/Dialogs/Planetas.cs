using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.Extensions.Logging;

namespace saganBot.Dialogs
{
    public class Planetas : ComponentDialog
    {
        private const string DoneOption = "Cancelar";

        private const string Selected = "value-companiesSelected";

        private readonly string[] _planetas = new string[]
        {
            "Mercurio", "Venus", "Marte", "Jupiter", "Saturno", "Urano", "Neptuno",
        };
        public Planetas() : base(nameof(Planetas))
        {
            //Definimos los Dialogos
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt))); //El Dialogo de las opciones
            AddDialog(new calcularEdad());
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[] //El dialogo en cascada con los dialogos a mostrar
            {
                ChoiceCardStepAsync,
                ShowCardStepAsync,
                //LoopStepAsync,
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
            var options = _planetas.ToList();

            if (list.Count is 0)
            {
                await stepContext.Context.SendActivityAsync(MessageFactory.Text("Planetas. Gran eleccion."), cancellationToken);
                await stepContext.Context.SendActivityAsync(MessageFactory.Text("Sabias que su origen etimologico significa errante?."), cancellationToken);
                await stepContext.Context.SendActivityAsync(MessageFactory.Text("Pues a simple viste parecen estrellas, pero a lo largo de los dias van 'moviendose' en relacion a las demas estrellas."), cancellationToken);
                message = $"Selecciona el planeta del que quieras saber algo, o `{DoneOption}` para finalizar.";
                options.Add(DoneOption);
            }
            else
            {
                await stepContext.Context.SendActivityAsync(MessageFactory.Text("Tambien puedo calcular tu edad según el planeta de tu eleccion."), cancellationToken);
                message = $"Puedes elegir otro planeta, " +$"o `{DoneOption}` para finalizar.";
                options.Add("Calculadora de Edad");
                options.Add(DoneOption);
            }

            if (list.Count > 0)
            {
                int conteo = list.Count
                    , i=0;
                do
                {
                    options.Remove(list[i]);
                    i++;
                } while (i<conteo);
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
            // Cards are sent as Attachments in the Bot Framework.
            // So we need to create a list of attachments for the reply activity.
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
                case "Mercurio":
                    // Display an Adaptive Card
                    //reply.AttachmentLayout = AttachmentLayoutTypes.Carousel; //le decimos que se envien en forma de carrucel
                    reply.Attachments.Add(Cards.Cards.mercurioCard()); //se añade al "carrucel" cada tarjeta, segun la bebida

                    break;
                case "Venus":
                    //reply.Attachments.Add(Cards.Cards)
                    reply.Attachments.Add(Cards.Cards.venusCard());
                    break;
                case "Marte":
                    reply.Attachments.Add(Cards.Cards.marteCard());
                    break;
                case "Jupiter":
                    reply.Attachments.Add(Cards.Cards.jupiterCard());
                    break;
                case "Saturno":
                    reply.Attachments.Add(Cards.Cards.saturnoCard());
                    break;
                case "Urano":
                    reply.Attachments.Add(Cards.Cards.uranoCard());
                    break;
                case "Neptuno":
                    reply.Attachments.Add(Cards.Cards.neptunoCard());
                    break;
                case "Calculadora de Edad":
                    return await stepContext.ReplaceDialogAsync(nameof(calcularEdad), null, cancellationToken);
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
                return await stepContext.ReplaceDialogAsync(nameof(Planetas), list, cancellationToken);
            }
            //return await stepContext.NextAsync(userProfile.PlanetSelected, cancellationToken);

            //return await stepContext.EndDialogAsync(); //fin del dialogo se regresa al dialogo principal
        }

        private async Task<DialogTurnResult> LoopStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // Retrieve their selection list, the choice they made, and whether they chose to finish.
            var list = stepContext.Values[Selected] as List<string>;
            var choice = (FoundChoice)stepContext.Result;
            var done = choice.Value == DoneOption;
            await stepContext.Context.SendActivityAsync(MessageFactory.Text("Salio"), cancellationToken);

            if (!done)
            {
                // If they chose a company, add it to the list.
                list.Add(choice.Value);
            }

            if (done)
            {
                // If they're done, exit and return their list.
                return await stepContext.EndDialogAsync(list, cancellationToken);
            }
            else
            {
                // Otherwise, repeat this dialog, passing in the list from this iteration.
                return await stepContext.ReplaceDialogAsync(nameof(Planetas), list, cancellationToken);
            }
        }
    }
}
