# RobotArchery
Solution to Jane Street's puzzle for Dec 2021

Puzzle --> https://www.janestreet.com/puzzles/robot-archery-index/

The puzzle asks us to estimate the possibility, under a list of conditions, that a participant to an archery competition wins.
Why Robot archery? The participats are robots of course! 

We could solve the problem applying the mathematical formula used for this kind of probabilistic calculations:
`*P4,4(1) = (-5/4)*(cos(1) + sin(1)) + (1/2)*e-1 + (e-1/2)*(cos(sqrt(3)/2) + 5/sqrt(3)*sin(sqrt(3)/2))*`

and get to the solution --> 0.18343765086

**This program is a logic exercice, we instead simulate many iterations (millions!) of the tournament to get the result, the more iterations we run and the closest we get to the result in the formula above.**

The program is written for **.NET 6** framework.

The whole execution can be as brief as <30s on consumer hardware, that's thanks to some design perks:

1. The Target for all iterations is only rendered once and reused, on top of that being the target a symmetric object only the left half is actually calculated and then it is mirrored, this **saves computational resources, memory, and cuts execution time** since there are way less -r-w operations.
2. Many simulations are run at once using **multithreading**, as many threads as supported by the CPU are run at a given time. This actually uses all the computational power of the running hardware and is way quicker that a vanilla For loop, there is a cap on the number of threads used to not overwhelm the host machine.

---------------------------------------
<br>

**DEMONSTRATION**

At program **launch**, just select how many simultaions you want to run --><br>
<img src="https://user-images.githubusercontent.com/96583994/187042184-24868019-b786-46c8-9c58-c65ebf6e7d6c.png" width="650"><br><br>

**Execution** --><br>
<img src="https://user-images.githubusercontent.com/96583994/187041815-998ae09c-ca49-4f66-a0be-2fca05ef7d60.png" width="650"><br><br>
<img src="https://user-images.githubusercontent.com/96583994/187041819-5f7c4a97-8d95-43ed-976a-7ded7a8c052a.png" width="650"><br><br>
<img src="https://user-images.githubusercontent.com/96583994/187041820-4ca7ec31-ca9b-45d9-97fc-05ae27c4378f.png" width="650"><br><br>

**Result** --><br>
<img src="https://user-images.githubusercontent.com/96583994/187041948-e5de1a9f-c3ae-40ec-a003-8bf52227f58a.png" width="650"><br><br>

------------------------------------
<br>


Give it a try, a compiled exevutable is available in the **Release** folder (just make sure you have .NET 6 installed). 

If you're running Linux remember to use `chmod u+x RobotArchery` before launching the program.



Thank you for visiting this project.
