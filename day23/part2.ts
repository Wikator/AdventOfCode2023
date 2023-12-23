import {readFileSync} from "fs";

class Coords {
    Y: number
    X: number

    constructor(Y: number, X: number) {
        this.X = X;
        this.Y = Y;
    }
}

function getLines(filePath: string): string[] {
    const file: string = readFileSync(filePath, 'utf-8');
    return file.split('\r\n');
}


function findStart(lines: string[]): Coords {
    for (let i: number = 0; i < lines.length; i++) {
        const index: number = lines[i].indexOf('.');

        if (index != -1)
            return new Coords(i, index);
    }

    throw Error();
}

function findEnd(lines: string[]): Coords {
    for (let i: number = lines.length - 1; i >= 0; i--) {
        const index: number = lines[i].lastIndexOf('.')

        if (index != 1)
            return new Coords(i, index)
    }

    throw Error()
}

function withinBoard(lines: string[], coords: Coords): boolean {
    return coords.Y >= 0 && coords.Y < lines.length && coords.X >= 0 && coords.X < lines[0].length
}

function longestPath(lines: string[], endCoords: Coords, startCoords: Coords, longest: number = 1): number {
    lines[startCoords.Y] =
        lines[startCoords.Y].substring(0, startCoords.X) +
        '#' +
        lines[startCoords.Y].substring(startCoords.X + 1);

    const possibleNextPaths: Coords[] = [
        new Coords(startCoords.Y - 1, startCoords.X),
        new Coords(startCoords.Y, startCoords.X + 1),
        new Coords(startCoords.Y + 1, startCoords.X),
        new Coords(startCoords.Y, startCoords.X - 1)
    ];

    return possibleNextPaths.reduce((acc: number, nextPath: Coords): number => {
        if (!withinBoard(lines, nextPath))
            return acc;

        if (nextPath.X == endCoords.X && nextPath.Y == endCoords.Y && acc < longest)
            return longest;

        if (lines[nextPath.Y][nextPath.X] === '#')
            return acc;


        const continuedPath: number = longestPath(lines.map((line: string) => line.slice()),
            endCoords, nextPath, longest + 1)

        return continuedPath > acc ? continuedPath : acc;
    }, 0);
}

const lines: string[] = getLines('data/input.txt')
const start: Coords = findStart(lines);
const end: Coords = findEnd(lines);
console.log(longestPath(lines, end, start))
