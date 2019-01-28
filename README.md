# predictorv2

The predictive element was replaced with a system to predict the goal difference and another to convert that to a likelihood for each score line, 
so the one with highest probability can be picked.

A linear regression algorithm is used for the predictive element, 
where attributes from all previous games (including xG for and against) were put into a matrix, X, 
as well as the goal difference from each of those into a result matrix, y, and then a calculation is done to obtain a vector, w, 
to multiply the test game’s attributes by to get a predicted goal difference.

The probability of each score happening is calculated by multiplying together;

The probability that the goal difference of a certain score will happen - given the expected GD 
The probability of the home team scoring and conceding n goals 
The probability of the away team scoring and conceding n goals
 Much quicker as it uses a relatively simple method, if matrices can be handled easily and improves on the previous version.
