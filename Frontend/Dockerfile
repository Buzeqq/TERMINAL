FROM node:22 as build
WORKDIR /app
COPY . .
RUN npm install
RUN npm install -g @angular/cli
RUN ng build -c production

FROM nginx:latest as production
COPY --from=build /app/dist/terminal /usr/share/nginx/html

EXPOSE 80
