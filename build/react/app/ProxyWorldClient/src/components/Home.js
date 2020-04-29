import React, {useEffect} from 'react';

export function Home() {
  useEffect(() => {

  }, []);

  return (
    <div className="text-dark">

      {/* Research Paper Area */}
      <div class="container">
        <div class="row">
          <div class="col-12 m-auto py-2">
            <h1 class="text-center">Sam Houston State University</h1>
            <h2 class="text-center">Cache Poisoning Research</h2>
          </div>
          <hr class="w-100" />
          <div class="col-12 py-2">
            <h3 class="text-left py-2">Contributors</h3>
            <ul>
              <li>
                <h4>Dr. Wei, Mingkui</h4>
                <h5>Computer Science Associate Professor</h5>
                <p>
                  
                </p>
              </li>
              <li>
              <h4>Nicholas Jones</h4>
              <h5>Computer Science Student</h5>
                <p>
                  My role started with the creation of a <b>droplet on digital ocean</b>. I've previously used digital ocean as an IaaS (Infrastruction as a Service)
                  to host web services for small businesses. It allows for the rapid deployment of applications and uses virtual resource allocation making it
                  extremely easy to upscale as needed. One additional perk is the $50 DigitalOcean credit a student can recieve by creating a GitHub Pro account with their student email.
                  The next role I took on was making the <b>test environment portable</b>. For this I used docker to create an automated docker-compose script to pull all the
                  dependencies automatically. The steps of how to make your own local environment are in the ReadMe.md on the GitHub Repository.
                  In additional to taking on the leadership role to ensure the progression of the project, I also created the tools HttpCacheHelper, HttpCachePython, and TcpClient.
                  These tools will be used to automate the testing of various aspects of http cache vulnerabilities. Originally, I created HttpCacheHelper but, c# automatically enforces ASCII only characters with that library.
                  Then i started the HttpCachePython but, still there was some automated blocking and similar issues with that. Lastly, I created the TCPClient Tool which builds and send the Http Request at Layer 4 which removes the
                  automated c# protections and allows for use to perform testing on special characters in unicode and similar.
                </p>
              </li>
            </ul>
          </div>
          <hr class="w-100" />
        </div>
      </div>

      <div class="py-3">
        <h3 class="text-left py-2">Introduction</h3>
        <div class="py-5">

          <h4>What is Cache Poisoning?</h4>
          <p>
            
          </p>

          <h4>The Operating System: IaaS</h4>
          <p>
            The operating system used during this project was Fedora31 hosted on digital ocean as a 'droplet'. Resource allocated requires that there be 1 core with a minimum of 2GB of required for the Node Package Manager. Also, the server requires that the Domain Settings be configured according to the github repository ReadMe.md file.
          </p>

          <h4>The Reverse Proxy: Nginx</h4>
          <p>
            Nginx was chosen out of preference and is not required specifically to run this experiement. Any proxy can be used to conduct the experiments such as apache or squid. However, Nginx was used during this experiment and it will be easier to recreate using the documentation provided for nginx.
          </p>

          <h4>Creating the daemons: Systemd</h4>
          <p>
            On the server, daemons were created using Systemd. These daemons ensure that the applications continously run on the server. The configurations are stated in the Github documentation.
          </p>
    
          <h4>Creating the Containers: Docker</h4>
          <p>
            Some computer science students are professionals have limited System Administration knowledge making it difficult to create a dedicated server for the experiments. Additionally, sharing a server for experiements with a group can lead to collisions in testings. To address these issues I created a Docker containers for the applications using docker compose. This allows for testing to be performed in isolation. The steps for creating the docker network is availabe on the github repository.
          </p>

          <h4>The Experimental Group: Web Applications</h4>
          <ul>
            <li>
              <h5>React</h5>
              <h6>React.js Using .NET Core 3.1</h6>
              <p>
                .Net Core 3.1 can be used as an API like many others in this list to serve a SPA (Single Page Application) using client side frameworks such as React, Angular, Vue and more. However, after our initial testing we acknowledged that client side javascript applications do not have access to HTTP Request Headers unless passed from the Backend Applications. The react applications however was left to serve as documentation and possible future testing.
              </p>
            </li>
            <li>
              <h5>Razor Pages</h5>
              <h6>Razor Pages Using .NET Core 3.1</h6>
              <p>
                .Net Core 3.1 is primarily written in C# which is a high level programming language developed by Microsoft. C# is very similar to java's syntax with minor difference such as the camel casing of methods. .Net is partially compiled and partially interpreted and runs on the .NET runtime environment similar to java's runetime environment. 
              </p>
            </li>
            <li>
              <h5>Django</h5>
              <h6>Using Python 3.8 and Gunicorn</h6>
              <p>
                
              </p>
            </li>
            <li>
              <h5>Flask</h5>
              <h6>Using Python 3.8 and Gunicorn</h6>
              <p>
                
            </p>
            </li>
            <li>
              <h5>Ruby</h5>
              <h6>Ruby on Rails Using Ruby 2.7 and Unicorn</h6>
              <p>
                
              </p>
            </li>
            <li>
              <h5>Spring</h5>
              <h6>Sring x.x Using Gradle</h6>
              <p>
                
              </p>
            </li>
            <li>
              <h5>Laravel</h5>
              <h6>Larval using PHP</h6>
              <p>
                
              </p>
            </li>
            <li>
              <h5>Phoenix</h5>
              <h6>Phoenix using ...</h6>
              <p>
                Phoenix is a web framework that uses the elixir language with the erlang Virtual Machine. Phoenix claims that it "brings back the simplicity and joy in writing modern web applications by mixing tried and true technologies with a fresh breeze of functional ideas.". The platform also claims high scalability, fault-tolerance, and envelops functional programming.
              </p>
            </li>
            <li>
              <h5>Express</h5>
              <h6>Express.js using Javascript and Node.js</h6>
              <p>
                 Express is a minimal and flexible Node.js web application framework that provides a robust set of features for web and mobile applications. Express is frequently used as an API with a javascript client library such as react or angular making the fullstack natively using javascript. There are many framework built on express such as Feathers, Kraen, Sails and much more.
              </p>
            </li>
          </ul>

          <h4>The Control Group: (XSS, Nginx, HTTP Headers)</h4>
          <p>
            XSS (Cross-Site Scripting) will be used to manipulated the Nginx Cache to cause following users to be poisoned and vulnerable to attacks.
          </p>
           <p>
            HTTP Headers will be used to test if XSS attacks can be injected through abnormal means such as duplicate headers, special non-ascii characters, and more.
          </p>
          <p>
            Nginx is part of the control group due to it's cache settings. We will be manipulating the cache settings after each round of testing to see if these settings effect the results of the experiments.
          </p>

        </div>
      </div>
      <hr class="w-100" />

      <div class="py-3">
        <h3 class="text-left py-2">Hypothesis</h3>
        <div class="py-5">
          <ol>
            <li>We believe that various frameworks are going to have built in defences against certain cache poisoning attacks and built in vulernabilities against other types of cache poisoning attacks.</li>
            <li>Web application defenses are going to be targeted at common character patterns as opposed to rare patterns such as greek characters and similar.</li>
            <li>The web frameworks will have more vulnerabilities with the initial configurations than after the configurations are updated to include additional security.</li>
            <li>Once a vulernability is detected, that vulernability will require minimum experience and knowledge to launch an attack.</li>
            <li>The XSS attacks will use javascript or a javascript library to manipulate the DOM of a user using cache poisoning attack or similar.</li>
          </ol>
        </div>
      </div>
      <hr class="w-100" />

      <div class="py-3">
        <h3 class="text-left py-2">Experiment</h3>
        <div class="py-5">
          <h5>Creation of an HTTP Cache Tool to run test against the experiemental group using the control group</h5>
          <ol>
            <li>Test duplicate headers effect on the applications and how they respond</li>
            <li>Test of path</li>
            <li>Test of functions (Desync) - Transfer-Encoding and Content-Length both present, different server will treat them differently. Will changing a certain header have an adverse effect on another headers security within the web application or proxy.</li>
            <li>Test of special characters- will greek characters, roman character, chinese characters effect how the applications process XSS attacks</li>
            <li></li>
          </ol>
        </div>
      </div>
      <hr class="w-100" />

      <div class="py-3">
        <h3 class="text-left py-2">Results</h3>
        <div class="py-5">
          <h4>Web Application 01 - Susceptible to ...</h4>
          <p>
            
          </p>
        </div>
      </div>
      <hr class="w-100" />

      <div class="py-3">
        <h3 class="text-left py-2">Conclusion</h3>
        <div class="py-5">
          <h4>The Vulerabilities Varied Between Web Applications ...</h4>
          <p>
            
          </p>
        </div>
      </div>
      <hr class="w-100" />
      
      <div class="py-3">
        <h3 class="text-left py-2">Sources</h3>
        <div class="py-5">
          <h4>Cache Research Source 01</h4>
          <p>
            
          </p>
        </div>
      </div>
      <hr class="w-100" />

        
        
      <div class="py-3">
        <h3 class="text-left py-2">Research Area</h3>
        <div>
          <h4>Navigation Object Output</h4>
          <p>
            {navigator.userAgent} <br />
            {navigator.language} <br />
            {navigator.platform} <br />
            {navigator.appName} <br />
          </p>
        </div>
      </div>

    </div>
  );
}


