﻿using System;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.AspNetCore;
using MQTTnet.Server;
using SenseHome.Broker.Services.Api;
using SenseHome.Broker.Utility;
using SenseHome.Common.Exceptions;
using SenseHome.DataTransferObjects.Authentication;


namespace SenseHome.Broker.Services.Connection
{
    public class MqttConnectionService : IMqttConnectionService
    {
        private IMqttServer mqttServer;
        private readonly IApiService apiService;
        private readonly BrokerEventTopics eventTopics;

        public MqttConnectionService(IApiService apiService, BrokerEventTopics eventTopics)
        {
            this.apiService = apiService;
            this.eventTopics = eventTopics;
        }


        public void ConfigureMqttServer(IMqttServer mqttServer)
        {
            this.mqttServer = mqttServer;
            mqttServer.ClientConnectedHandler = this;
            mqttServer.ClientDisconnectedHandler = this;
        }

        public void ConfigureMqttServerOptions(AspNetMqttServerOptionsBuilder options)
        {
            options.WithConnectionValidator(this);
        }

        public async Task HandleClientConnectedAsync(MqttServerClientConnectedEventArgs eventArgs)
        {
            var serializedData = Utf8Json.JsonSerializer.Serialize(new { ClientId = eventArgs.ClientId });
            var message = new MqttApplicationMessageBuilder().WithTopic(eventTopics.NewClientConnected)
                                                             .WithPayload(serializedData)
                                                             .Build();
            await mqttServer.PublishAsync(message);
        }

        public async Task HandleClientDisconnectedAsync(MqttServerClientDisconnectedEventArgs eventArgs)
        {
            var serializedData = Utf8Json.JsonSerializer.Serialize(new { ClientId = eventArgs.ClientId });
            var message = new MqttApplicationMessageBuilder().WithTopic(eventTopics.NewClientDisconnected)
                                                             .WithPayload(serializedData)
                                                             .Build();
            await mqttServer.PublishAsync(message);
        }

        public async Task ValidateConnectionAsync(MqttConnectionValidatorContext context)
        {
            System.Console.WriteLine("Incoming Request");
            await Task.Run(() => {
                context.SessionItems.Add("bearer", "");
                context.ReasonCode = MQTTnet.Protocol.MqttConnectReasonCode.Success;
            });
            // try
            // {
            //     var loginDto = new UserLoginDto { Name = context.Username, Password = context.Password };
            //     var tokenDto = await apiService.LoginAsync(loginDto);
            //     try
            //     {
            //         var userDto = await apiService.GetUserProfileAsync(tokenDto);
            //         if (userDto.Id != context.ClientId)
            //         {
            //             context.ReasonCode = MQTTnet.Protocol.MqttConnectReasonCode.ClientIdentifierNotValid;
            //         }
            //         else
            //         {
            //             context.SessionItems.Add("bearer", tokenDto.Bearer);
            //             context.ReasonCode = MQTTnet.Protocol.MqttConnectReasonCode.Success;
            //         }
            //     }
            //     catch (UnauthorizedException)
            //     {
            //         context.ReasonCode = MQTTnet.Protocol.MqttConnectReasonCode.NotAuthorized;
            //     }
            //     catch (NotFoundException)
            //     {
            //         context.ReasonCode = MQTTnet.Protocol.MqttConnectReasonCode.BadUserNameOrPassword;
            //     }
            //     catch (Exception)
            //     {
            //         context.ReasonCode = MQTTnet.Protocol.MqttConnectReasonCode.UnspecifiedError;
            //     }
            // }
            // catch (UnauthorizedException)
            // {

            //     context.ReasonCode = MQTTnet.Protocol.MqttConnectReasonCode.BadUserNameOrPassword;
            // }
        }
    }
}
