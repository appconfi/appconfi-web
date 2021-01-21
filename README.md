# Appconfi

Appconfi provides a toggle management for the applications.

## Quick start
Install Docker and run a appconfi-web container with the following command.

### Build docker image 

```bash
  docker build -t appconfi:latest .
```

### Run your docker container

```bash
docker run \
  --interactive \
  --tty \
  --detach \
  --env  #see secrets below
  --publish 80:80 \
  --name appconfi-web \
  appconfi
```


### Secrets
Setup secrets using environments variables

```json
{
  "JWT": {
    "Secret": "---" // Secret auth key
  },
  "SendGrid": { // Sendgrid credentials to send emails
    "ApiId": "---",
    "ApiKey": "---"
  },
  "ConnectionStrings": {
    "DefaultConnection": "---" // Postgres connection string
  },
  "BaseUrl": "---", // Your custom url
  "Authentication": { // Google OAuth credentials
    "Google": {
      "ClientId": "---",
      "ClientSecret": "---"
    }
  }
}
```


## How do I contribute

There are lots of ways to contribute to the Appconfi project including testing, reporting bugs, and contributing code.

All code submissions will be reviewed and tested by the Appconfi team, and then will be merged into the source.


## License

**appconfi-web** is licensed under the MIT license.
