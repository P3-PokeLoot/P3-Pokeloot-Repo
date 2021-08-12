FROM node:latest AS build
WORKDIR /src
COPY . .
RUN npm install
RUN npm install ngx-pagination
RUN npm install jquery
RUN npm install bootstrap
RUN npm run build

FROM nginx:alpine
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=build /src/dist/P2Angular/ /usr/share/nginx/html
