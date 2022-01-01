using NewLife;

namespace RocketmqComsumer
{
    public class EventDispathcer : IDisposable
    {
        NewLife.RocketMQ.Consumer? consumer;
        readonly ILogger<EventDispathcer> logger;
        public EventDispathcer(ILogger<EventDispathcer> logger)
        {
            this.logger = logger;
        }
        public void Dispose()
        {
            Stop();
        }

        public void Start()
        {
            //测试消费消息
            consumer = new NewLife.RocketMQ.Consumer
            {
                Topic = "DESKTOP-ACRFQSI",
                Group = "CID_ONSAPI_OWNER",
                NameServerAddress = "192.168.1.194:9876",
                //设置每次接收消息只拉取一条信息
                BatchSize = 1,
                //FromLastOffset = true,
                //SkipOverStoredMsgCount = 0,
                //BatchSize = 20,
                //Log = NewLife.Log.XTrace.Log,
            };
            consumer.OnConsume = (q, msgArr) =>
            {
                string mInfo = $"BrokerName={q.BrokerName},QueueId={q.QueueId},Length={msgArr.Length}";
                Console.WriteLine(mInfo);
                foreach (var item in msgArr.ToList())
                {
                    string msg = $"消息：msgId={item.MsgId},key={item.Keys}，产生时间【{item.BornTimestamp.ToDateTime()}】，内容>{item.Body.ToStr()}";
                    Console.WriteLine(msg);
                }
                // return false;// 通知消息队：不消费消息
                return true; // 通知消息队：消费了消息
            };

            consumer.Start();
            Task.CompletedTask.GetAwaiter().GetResult();
        }


        public void Stop()
        {
            logger.LogInformation("atop");
            consumer?.Stop();
        }
    }
}
