import {getLines, sumOfExtrapolatedValues} from "./shared";

const history: string[] = getLines('data/input.txt')

const extrapolateForward: ((d: number[][]) => number) = (sequences: number[][]): number => {
    sequences[sequences.length - 1].push(0);
    for (let i: number = sequences.length - 2; i >= 0; i--) {
        sequences[i].push(sequences[i][sequences[i].length - 1] + sequences[i + 1][sequences[i + 1].length - 1]);
    }
    return sequences[0][sequences[0].length - 1];
}

const result: number = sumOfExtrapolatedValues(history, extrapolateForward);
console.log(result);
