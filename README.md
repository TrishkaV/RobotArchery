# RobotArchery
Solution to Jane Street's puzzle for Dec 2021

Puzzle --> https://www.janestreet.com/puzzles/robot-archery-index/

The puzzle asks us to estimate the possibility, under a list of conditions, that a participant to an archery competition wins.
Why Robot archery? The participats are robots of course! 

We could solve the problem applying the mathematical formula used for this kind of probabilistic calculations:
`*P4,4(1) = (-5/4)*(cos(1) + sin(1)) + (1/2)*e-1 + (e-1/2)*(cos(sqrt(3)/2) + 5/sqrt(3)*sin(sqrt(3)/2))*`

and get to the solution --> 0.18343765086

**This program is a logic exercice, we instead simulate many iterations (millions!) of the tournament to get the result, the more iterations we run and the closest we get to the result in the formula above.**



The whole execution can be as brief as <30s on consumer hardware, that's thanks to some design perks:

1. The Target for all iterations is only rendered once and reused, on top of that being the target a symmetric object only the left half is actually calculated and then it is mirrored, this saves computational resources, memory, and cuts execution time since there are way less -r-w operations.
2. Many simulations are run at once using Multithreading, as many threads as supported by the CPU are run at a given time. This actually uses all the computational power of the running hardware and is way quicker that a vanilla For loop, there is a cap on the number of threads used to not overwhelm the host machine.


Give it a try, a compiled exevutable is available in the Release folder.

