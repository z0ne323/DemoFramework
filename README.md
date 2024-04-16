# DemoFramework (C2 Framework made in C#)

Repo storing the sample code used and learn during the course offered by Zero-Point Security !

I highly recommend the course, I've learned great things in there, you can find the link [here](https://training.zeropointsecurity.co.uk/courses/c2-development-in-csharp).

I'll give you a bit of an overview about what's in there but feel free to take this course to learn more in depth, you can also check around the code to get an overview yourself about what's covered. 

Before diving into the features, I just like to give a heads up, per say the project in itself doesn't run on the last .NET version. Even though we're using an out-of-support .NET throughout the course, because of the cross compatibility feature of Visual Studio and more importantly the course content, I still think that it's valuable to go for it as of today's writing. On top of this, since I learned so many amazing stuffs from this project, I rightfully think that even if it's "out-of-date" on certain aspect of the code structure, there's still a lot of things to learn from this course !

Alright, let's start!

# Components of the C2 Framework

The C2 Framework is set up in a way that allows both the operator and the implant (agent if you want) to communicate with our program. The program in itself is composed of one big part called `TeamServer`, in there, we actually have:
- `User API`
- `Listener`
-  `DI Container` / `IService` that allows `TeamServer` to work effectively, since the two parts mentioned above are able to communicate with one to another, which indirectly makes a bridge between the agent being deployed on the target and the operator controlling it through the `API`!

## TeamServer

Handling multiple tasks through `User API`:

Agents:
- `GET /Agents`, allows us to fetch the agents
- `GET /Agents/{agentId}`, allows us to fetch specific details about one particular agent, by providing an `agentId`
- `POST /Agents/{agentId}`, allows us to create an agent, by providing an `agentId`
- `GET /Agents/{agentId}/tasks`, allows us to fetch the tasks from a particular agent, by providing an `agentId`
- `GET /Agents/{agentId}/tasks/{taskId}`, allows us to fetch a specific task by providing a `taskId`, from a particular agent, by providing an `agentId`

Listeners:
- `GET /Listeners`, allows us to fetch the listeners
- `POST /Listeners`, allows us to create a listener 
- `GET /Listeners/{name}`, allows us to get details about a specific listener, by providing the `name`
- `DELETE /Listeners/{name}`, allows us to delete a specific listener, by providing the `name`

If you don't modify the project, when running our program, you'll see the default url for our `API` documentation on `Swagger` will seat at `http://localhost:5000/swagger/index.html`. (Should launch automatically if you run the program)

## Agent / Implant

Second big part of the framework that is actually being executed on the target machine. There is various capabilities provided through the `User API` and when passed through there, commands can get executed on the target machine through our connection from and to the Agent. I'll list the commands / features present in our agent capabilities thanks to our almighty `DemoFramework`:

- `pwd cd & ls`
- `mkdir & rmdir`
- `ps`
- `whoami, shell & run`
- `execute-assembly`
- `make-toekn, rev2self`
- `steal-token`
- `self-inject`
- `remote-inject`
- `spawn-inject`

Hope this was a comprehensive overview of the project, I'd like to personnaly thank `Rasta Mouse` (A.K.A Daniel Duggen, founder of Zero-Point Security), for all the knowledges he brought to me through this course. This allowed me to sharpen a little bit more my C# skills, learn more about C2 development and just have an amazing time doing this project along him on the side ! 