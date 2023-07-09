FROM node:20-alpine as build
WORKDIR /app
COPY . .
RUN npm install
RUN npm run build --omit=dev

FROM nginx:latest as production
COPY --from=build /app/dist/client /usr/share/nginx/html

EXPOSE 80
