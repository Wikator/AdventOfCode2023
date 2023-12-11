import {readFileSync} from "fs";

export function getLines (filePath: string): string[] {
    const file: string = readFileSync(filePath, 'utf-8');
    return file.split('\r\n');
}

export function sumOfExtrapolatedValues(history: string[], extrapolate: ((d: number[][]) => number)): number {
    return history.reduce((acc: number, curr: string): number => {
        const sequences: number[][] = [curr.split(' ').map((s: string): number => parseInt(s))];

        while (sequences[sequences.length - 1].filter((n: number): boolean => n != 0).length !== 0)
        {
            sequences.push([]);
            for (let i: number = 0; i < sequences[sequences.length - 2].length - 1; i++) {
                sequences[sequences.length - 1].push(sequences[sequences.length - 2][i + 1] - sequences[sequences.length - 2][i]);
            }
        }
        return acc + extrapolate(sequences);
    }, 0);
}
