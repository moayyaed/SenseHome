# SenseHome

**How to run**

- `docker-compose build`
- `docker-compose up -d`

**Connect Web Client**

- Clone the web client from [https://github.com/sensehome/WebClient](https://github.com/sensehome/WebClient)
- Update `API_SERVER` to `http://localhost:5000/api` [in this line](https://github.com/sensehome/WebClient/blob/6619923497d27111403d59050e9cba6bc4e6a148/src/app/services/api.service.ts#L10)
- Update `AGENT_SERVER` to `http://localhost:4000` [in this line](https://github.com/sensehome/WebClient/blob/6619923497d27111403d59050e9cba6bc4e6a148/src/app/services/agent.service.ts#L22)
- `npm install`
- `npm start`
