import {readFileSync} from "fs";


enum Direction {
    Up,
    Right,
    Down,
    Left
}

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

function matchDirection(char: string, direction: Direction): boolean {
    switch (direction) {
        case Direction.Left:
            return char === '<';
        case Direction.Down:
            return char === 'v';
        case Direction.Right:
            return char === '>';
        case Direction.Up:
            return char === '^';
    }
}

function getCoords(coords: Coords, direction: Direction): Coords {
    switch (direction) {
        case Direction.Left:
            return new Coords(coords.Y, coords.X - 1);
        case Direction.Down:
            return new Coords(coords.Y + 1, coords.X);
        case Direction.Right:
            return new Coords(coords.Y, coords.X + 1);
        case Direction.Up:
            return new Coords(coords.Y - 1, coords.X);
    }
}

function withinBoard(lines: string[], coords: Coords): boolean {
    return coords.Y >= 0 && coords.Y < lines.length && coords.X >= 0 && coords.X < lines[0].length
}

function longestPath(lines: string[], endCoords: Coords, startCoords: Coords, longest: number = 1): number {
    lines[startCoords.Y] =
        lines[startCoords.Y].substring(0, startCoords.X) +
        '#' +
        lines[startCoords.Y].substring(startCoords.X + 1);

    const possibleDirections: Direction[] = [
        Direction.Up,
        Direction.Right,
        Direction.Down,
        Direction.Left
    ];

    return possibleDirections.reduce((acc: number, direction: Direction): number => {
        const coords: Coords = getCoords(startCoords, direction);

        if (!withinBoard(lines, coords))
            return acc;

        if (coords.X == endCoords.X && coords.Y == endCoords.Y && acc < longest)
            return longest;

        if (lines[coords.Y][coords.X] === '#' ||
            (lines[coords.Y][coords.X] !== '.' && !matchDirection(lines[coords.Y][coords.X], direction))) {
            return acc;
        }

        const continuedPath: number = longestPath(lines.map((line: string) => line.slice()),
            endCoords, coords, longest + 1)

        return continuedPath > acc ? continuedPath : acc;
    }, 0);
}

const lines: string[] = getLines('data/input.txt')
const start: Coords = findStart(lines);
const end: Coords = findEnd(lines);
console.log(longestPath(lines, end, start))
