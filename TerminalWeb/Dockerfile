FROM node:20-alpine as development

WORKDIR /app

RUN npm install -g @angular/cli@15.2.0

COPY . .

RUN npm install --include=dev

# check if angular is installed
RUN ng version

EXPOSE 4200
CMD ["ng", "serve", "--poll", "2000", "--configuration", "development", "--host", "0.0.0.0", "--disable-host-check"]


