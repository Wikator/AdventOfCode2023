import {getLines, sumOfExtrapolatedValues} from "./shared";

const history: string[] = getLines('data/input.txt')

const extrapolateBackwards: ((d: number[][]) => number) = (sequences: number[][]): number => {
    sequences[sequences.length - 1].unshift(0);
    for (let i: number = sequences.length - 2; i >= 0; i--) {
        sequences[i].unshift(sequences[i][0] - sequences[i + 1][0]);
    }
    return sequences[0][0];
}

const result: number = sumOfExtrapolatedValues(history, extrapolateBackwards);
console.log(result);
