FROM node:18-alpine AS build

WORKDIR /app

COPY package.json yarn.lock ./

RUN yarn install

COPY . .

RUN node --version && yarn --version && npx ng version

RUN yarn build --configuration production

FROM nginx:alpine

COPY --from=build /app/dist/sales-prediction-app/browser /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
