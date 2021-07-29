//this is a class that Mocks shiny statistic data

import { MockShinyData } from "./IMockShinyData";

//we will need to bring username to have more precise leaderboards - requires a join of tables
export const MockShinyStatisticData:MockShinyData[] = [
    {userId:1,
    username:'user1',
    countOfShiny:2},
    {userId:2,
    username:'user2',
    countOfShiny:20},
    {userId:3,
    username:'user3',
    countOfShiny:100},
    {userId:4,
    username:'user4',
    countOfShiny:4},
    {userId:5,
    username:'user5',
    countOfShiny:3},
    {userId:6,
    username:'user6',
    countOfShiny:6},
]