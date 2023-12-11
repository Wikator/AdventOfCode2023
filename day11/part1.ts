import {getLines, Pair, getPairs} from "./shared";

function expandGalaxiesHorizontal(lines: string[], currentIndex: number = 0): string[] {
    if (currentIndex === lines.length)
        return lines;

    if (lines[currentIndex].split('').every((char: string): boolean => char === '.'))
        return expandGalaxiesHorizontal(
            [...lines.slice(0, currentIndex),
                lines[currentIndex],
                ...lines.slice(currentIndex)],
            currentIndex + 2);

    return expandGalaxiesHorizontal(lines, currentIndex + 1);
}

function expandGalaxiesVertically(lines: string[], currentIndex: number = 0): string[] {
    if (currentIndex === lines[0].length)
        return lines;

    const allChars: string[] = lines.map((l: string) => l[currentIndex]);

    if (allChars.every((char: string): boolean => char === '.'))
        return expandGalaxiesVertically(lines.
            map((currLine: string) => currLine.split('')
                .flatMap((currChar: string, index: number): string[] => (index == currentIndex) ? [currChar, currChar] : [currChar]).join('')),
            currentIndex + 2);

    return expandGalaxiesVertically(lines, currentIndex + 1);
}

const lines: string[] = getLines('data/input.txt');
const expandedGalaxies: string[] = expandGalaxiesHorizontal(expandGalaxiesVertically(lines));
const pairs: Pair[] = getPairs(expandedGalaxies);

const result: number = pairs.reduce((acc: number, curr: Pair) => {
    return acc + Math.abs(curr.galaxy2XIndex - curr.galaxy1XIndex) + Math.abs(curr.galaxy2YIndex - curr.galaxy1YIndex);
}, 0);

console.log(result);
