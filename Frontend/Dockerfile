FROM node:20.2 as build
WORKDIR /app
COPY . .
RUN npm install
RUN npm install -g @angular/cli
RUN ng build -c production

FROM nginx:latest as production
COPY --from=build /app/dist/terminal-client /usr/share/nginx/html

EXPOSE 80
