
### Create Service Bus Namespace and simple Queue test

- Create *Service Bus Namespace* (Standard)
    - Service Bus -> Entities -> Queues
        - **+ Queue**
        - Give the queue a name and leave other values as is
        - **Create**
     - Go to Queues -> Click on newly created *queue*
     - *Service Bus Explorer*
       - *Send Messages*
       - Send a couple of messages
     - Go back to *Overview* and examine the *Message Counts* *Active*
     - Back in *Service Bus Explorer* Click *Peek from start*
         (This will show you the messages currently in the queue, but not process them)
     - Switch from *Peek Mode* to *Receive Mode*
         - Select *Receive messages* and choose *ReceiveAndDelete*
         - Click *Receive*
      - Now you get the first message submitted to the queue and the active message count show now be decremented by 1

  