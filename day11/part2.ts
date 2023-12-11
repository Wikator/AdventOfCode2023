import {getLines, Pair, getPairs} from "./shared";

function expandGalaxiesHorizontal(lines: string[], expandIndexes: number[] = [], currentIndex: number = 0): number[] {
    if (currentIndex === lines.length)
        return expandIndexes;


    if (lines[currentIndex].split('').every((string: string): boolean => string === '.'))
        return expandGalaxiesHorizontal(lines, [...expandIndexes, currentIndex], currentIndex + 1);

    return expandGalaxiesHorizontal(lines, expandIndexes, currentIndex + 1);
}

function expandGalaxiesVertically(lines: string[], expandIndexes: number[] = [], currentIndex: number = 0): number[] {
    if (currentIndex === lines[0].length)
        return expandIndexes;

    const allChars: string[] = lines.map((l: string) => l[currentIndex]);

    if (allChars.every((string: string): boolean =>  string === '.'))
        return expandGalaxiesVertically(lines, [...expandIndexes, currentIndex], currentIndex + 1);

    return expandGalaxiesVertically(lines, expandIndexes, currentIndex + 1);
}

const lines: string[] = getLines('data/input.txt');
const horizontalExpands: number[] = expandGalaxiesHorizontal(lines);
const verticalExpands: number[] = expandGalaxiesVertically(lines);
const pairs: Pair[] = getPairs(lines);

const result: number = pairs.reduce((acc: number, curr: Pair) => {
    const numOfHorizontalExpands: number = (curr.galaxy1YIndex > curr.galaxy2YIndex) ? horizontalExpands.filter((h: number) => h < curr.galaxy1YIndex && h > curr.galaxy2YIndex).length : horizontalExpands.filter((h: number) => h > curr.galaxy1YIndex && h < curr.galaxy2YIndex).length;
    const numOfVerticalExpands: number = (curr.galaxy1XIndex > curr.galaxy2XIndex) ? verticalExpands.filter((v: number) => v < curr.galaxy1XIndex && v > curr.galaxy2XIndex).length : verticalExpands.filter((v: number) => v > curr.galaxy1XIndex && v < curr.galaxy2XIndex).length;
    return acc + Math.abs(curr.galaxy2XIndex - curr.galaxy1XIndex) + Math.abs(curr.galaxy2YIndex - curr.galaxy1YIndex) + 999999 * (numOfVerticalExpands + numOfHorizontalExpands);
}, 0)

console.log(result);
