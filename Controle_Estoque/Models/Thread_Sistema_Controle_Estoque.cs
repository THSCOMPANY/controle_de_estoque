using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;//First of all import System.Threading namespace, it plays an important role in creating a thread in your program as you have no need to write the fully qualified name of class everytime.

namespace Controle_Estoque
{
    class Thread_Sistema_Controle_Estoque
    {
        //How to Create Threads in C#

        /*  In C#, a multi-threading system is built upon the Thread class, which encapsulates the execution of threads. This class contains several methods and properties
            which helps in managing and creating threads and this class is defined under System.Threading namespace. The System.Threading namespace provides classes and 
            interfaces that are used in multi-thread programming.

        Some commonly used classes in this namespace are:
        
        Class Name

        Mutex       -->>    It is a synchronization primitive that can also be used for IPS (Interprocess synchronization).

        Monitor     -->>    This is class provides a mechanism that access objects in synchronize manner.

        Semaphore   -->>    This class is used to limit the number of threads that can access a resource or pool of resources concurrently.

        Thread      -->>    This class is used to creates and controls a thread, sets its priority, and gets its status.

        ThreadPool  -->>    This class provides a pool of threads than can be used to execute tasks, post work items, process asynchronous I/O, wait on behalf of other threads, and process timers.

        ThreadLocal -->>    This class provides thread-local storage of data.

        Timer       -->>    This class provides a mechanism for executing a method on a thread pool thread at specified intervals. You are not allowed no inherit this class.

        Volatile    -->>    This class contains methods for performing volatile memory operations.
         */

        public void trabalho01()
        {
            int i;

            for (i = 0; i < 10; i++)
            {
                i = i + 1;
            }
        }

        //Now, create and initialize the thread object in your createThread method.
        public void CreateThread()
        {
            Thread thread = new Thread(trabalho01);//Chamando um procedimento
        }

        //You can also use ThreadStart constructor for initializing a new instance.
        public void createThread02()
        {
            Thread thread = new Thread(new ThreadStart(trabalho01));
        }

        //Now you can call your thread object
        public void createThread03()
        {
            Thread thread = new Thread(trabalho01);
            thread.Start();
        }

        public void mythread01()
        {
            for (int z = 0; z < 3; z++)
            {
                Console.WriteLine("First Thread");
            }
        }
    }


    class TesteThread
    {
        public static void Principal()
        {
            //Creating object of Thread_Sistema_Controle_Estoque class
            Thread_Sistema_Controle_Estoque obj = new Thread_Sistema_Controle_Estoque();

            Thread thread = new Thread(new ThreadStart(obj.mythread01));
            thread.Start();
        }

    }

}
