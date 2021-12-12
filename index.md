# Sagan Bot

Un chat bot sobre astronomía creado con [Microsoft Bot Framework](https://dev.botframework.com/) capaz de enviar información a través de tarjetas y calcular edad relativa en otros planetas utilizando [Text recognizer](https://github.com/microsoft/Recognizers-Text)

## Capturas del Bot

Algunas capturas del bot con algunos fragmentos de código dados para contextualizar el funcionamiento de este

### Inicio del bot con tarjeta de bienvenida
![Inicio del bot](https://i.imgur.com/sydtUbp.png)

```c#
public static HeroCard GetWelcomeHeroCard()
        {
            var heroCard = new HeroCard
            {
                Title = "Sagan Bot",
                Subtitle = "El Bot de astronomia",
                Text = " Bienvenido(a). Estoy aqui para enseñarte un poco de astronomia",
                Images = new List<CardImage> { new CardImage("imagenInicio") },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.MessageBack, "Enseñame", value: "Empezar") },
            };

            return heroCard;
        }      
```

### Opciones dentro del bot
![Opciones](https://i.imgur.com/N8Hpgxe.png)


```c#
new PromptOptions
                  {
                      Prompt = MessageFactory.Text("¿Qué quieres aprender hoy?"), //Frase inicial de la pregunta
                      RetryPrompt = MessageFactory.Text("No entendi eso.  Por favor elija una opción de la lista."), //en caso de seleccionar una opcion inexistente se envia este mensaje
                      Choices = GetChoices(), //opciones
                  }, cancellationToken);
```            

```c#
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
```
### Opción "Estrellas"
![Estrellas A](https://i.imgur.com/7SCGjvN.png)

![Estrellas B](https://i.imgur.com/Xp3JPDU.png)
Con cada selección se retiran las opciones ya elegidas
```c#
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
```

```c#
 //Creamos el mensaje con las opciones dadas a el usuario
            var promptOptions = new PromptOptions
            {
                Prompt = MessageFactory.Text(message),
                RetryPrompt = MessageFactory.Text("No entendí eso, por favor elige uno de la lista."),
                Choices = ChoiceFactory.ToChoices(options),
            };
```

El resto del código y su funcionamiento es extenso, pero puede verse [aqui](https://github.com/RaymoEf/saganBot/blob/master/saganBot/Dialogs/estrellas.cs)

### Calculadora de edad

![Edad A](https://i.imgur.com/ELDLfeY.png)

![Edad B](https://i.imgur.com/cz8KWO6.png)

![Edad C](https://i.imgur.com/cwo7N1w.png)

```c#
 private static bool ValidateDate(string input, out string date, out string message)
        {
            
            date = null;
            message = null;

            try
            {
                var results = DateTimeRecognizer.RecognizeDateTime(input, Culture.Spanish);


                foreach (var result in results)
                {
                    

                    foreach (var resolution in resolutions)
                    {
                       
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
``` 
Validar fecha dada al bot por medio del DateTime Recognizer

```c#
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
``` 
Pide comfirmación de la fecha       

De nuevo, el código completo es extenso, por lo que se puede encontrar [aqui](https://github.com/RaymoEf/saganBot/blob/master/saganBot/Dialogs/calcularEdad.cs)
