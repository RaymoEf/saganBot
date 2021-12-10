// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.6.2

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using Microsoft.Recognizers.Text.DataTypes.TimexExpression;

using saganBot.CognitiveModels;

namespace saganBot.Dialogs
{
    public class MainDialog : ComponentDialog
    {
        private readonly UserState _userState;
        protected readonly ILogger Logger;
        public MainDialog(ConversationState conversationState, UserState userState, ILogger<MainDialog> logger)
             : base(nameof(MainDialog))
        {
            _userState = userState;

            AddDialog(new Planetas());
            AddDialog(new estrellas());
            AddDialog(new calcularEdad());
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                IntroStepAsync,
                ActStepAsync,
                FinalStepAsync,
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

            return await stepContext.PromptAsync(nameof(ChoicePrompt),
                  new PromptOptions
                  {
                      Prompt = MessageFactory.Text("¿Qué quieres aprender hoy?"), //Frase inicial de la pregunta
                      RetryPrompt = MessageFactory.Text("No entendi eso.  Por favor elija una opción de la lista."), //en caso de elegir una pcion inexistente se encia este mensaje
                      Choices = GetChoices(), //opciones
                  }, cancellationToken);
        }

        private async Task<DialogTurnResult> ActStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivitiesAsync(
          new Activity[] {
                new Activity { Type = ActivityTypes.Typing },
              //new Activity { Type = "delay", Value= 3000 },
          },
          cancellationToken);
            var reply = MessageFactory.Text("Sin eleccion");

            if (((FoundChoice)stepContext.Result).Value == "Estrellas")
            {
                await stepContext.Context.SendActivitiesAsync(new Activity[] { new Activity { Type = ActivityTypes.Typing }, }, cancellationToken);
                return await stepContext.BeginDialogAsync(nameof(estrellas), null, cancellationToken);
            }
            else if (((FoundChoice)stepContext.Result).Value == "Planetas")
            {
                await stepContext.Context.SendActivitiesAsync(new Activity[] {new Activity { Type = ActivityTypes.Typing },},cancellationToken);
                return await stepContext.BeginDialogAsync(nameof(Planetas), null, cancellationToken);
            }
            else if (((FoundChoice)stepContext.Result).Value == "Universo")
            {

            }
            else if (((FoundChoice)stepContext.Result).Value == "Calculadora de edad")
            {
                await stepContext.Context.SendActivitiesAsync(new Activity[] {new Activity { Type = ActivityTypes.Typing },},cancellationToken);
                return await stepContext.BeginDialogAsync(nameof(calcularEdad), null, cancellationToken);
            }
            else { }

            await stepContext.Context.SendActivitiesAsync(new Activity[] {new Activity { Type = ActivityTypes.Typing },},cancellationToken);
            await stepContext.Context.SendActivityAsync(reply, cancellationToken);

            return await stepContext.NextAsync(reply, cancellationToken);
        }

        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivitiesAsync(new Activity[] {new Activity { Type = ActivityTypes.Typing },},cancellationToken);
            return await stepContext.EndDialogAsync(null, cancellationToken);

        }

        private IList<Choice> GetChoices()
        {
            var Options = new List<Choice>()
            {
                new Choice() { Value = "Estrellas", Synonyms = new List<string>() { "Quiero conocer las estrellas","Algo sobre las estrellas", "Cuentame sobre las estrellas" } },
                new Choice() { Value = "Planetas", Synonyms = new List<string>() { "Algo sobre los planetas","Cuentame sobre los planetas" } },
                new Choice() { Value = "Calculadora de edad", Synonyms = new List<string>() { "Quiero calcular mi edad", "mi edad en otro planeta","conocer mi edad en otro planeta", "Calcular mi edad" } },
                new Choice() { Value = "Universo", Synonyms = new List<string>() { "Quiero conocer el universo", "Sobre el universo","Cuentame sobre el universo" } },
                new Choice() { Value = "Cancelar", Synonyms = new List<string>() { "Atras","Adios","Volver" } },
            };

            return Options;
        }
    }
}
