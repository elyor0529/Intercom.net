# intercom-net

.NET bindings for the [Intercom API](https://api.intercom.io/docs)

At the moment, this library only supports submitting Events. As I have more time I'll add more bindings and I'll add it to NuGet.

## Usage

### Events

    var client = new IntercomRestClient("appId", "apiKey");
    bool success = client.SubmitEvent(new IntercomEvent
    {
        EventName = "test",
        Email = "john.doe@example.com"
    });

