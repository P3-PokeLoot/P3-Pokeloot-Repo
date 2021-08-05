FROM node:latest AS build
WORKDIR /src
COPY . .
RUN npm run build --prod

FROM nginx:alpine
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=build /src/dist/P2Angular/ /usr/share/nginx/html