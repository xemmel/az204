
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

  
  ### Create a Topic

 - Inside your *Service Bus Namespace*
     - Create new *Topic*
        - Inside the newly created topic create a new subscription (getall)
        - Submit a new message using the *Service Bus Explorer* in your *topic* and verify that the subscription *getall* now has 1 message
        - Create a new subscription *denmarkonly*
         - Inside the new subscription
             -> Overview -> **+ Add Filter** 
                -> Give the filter a name *denmark* and insert (country = 'DK')
        - submit another message in the *topic* and verify that *getall* gets the message and *denmarkonly* do not
        - submit yet another message, this time go to *Custom Properties* *+ Add Custom Property* 
             - Key: country
             - Type: String
             - Value: DK
        - Send the message and verify that now both *getall* and *denmarkonly* gets the message (each get a cloned copy)