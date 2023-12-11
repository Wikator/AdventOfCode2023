import { getLines, SeedMap, getSeedMaps, Map } from './shared';

const lines: string[] = getLines('./data/input.txt');
const seeds: bigint[] = lines[0].split(' ').slice(1).map((s: string) => BigInt(s));
const seedMaps: SeedMap[] = getSeedMaps(lines.splice(2));
const groupedSeeds: bigint[][] = [];

for (let i: number = 0; i < seeds.length; i += 2) {
    groupedSeeds.push([seeds[i], seeds[i + 1]]);
}

let ans: bigint = null;

groupedSeeds.forEach((groupedSeed: bigint[]): void => {
    for (let i: bigint = BigInt(0); i < groupedSeed[1]; i += BigInt(1)) {

        const newSeed: bigint = seedMaps.reduce((seedMapAcc: bigint, seedMap: SeedMap): bigint => {
            let newValue: bigint = seedMapAcc;
            seedMap.maps.every((map: Map): boolean => {

                if (newValue > map.sourceRangeStart + map.range - BigInt(1) || newValue < map.sourceRangeStart)
                    return true;

                const distance: bigint =  seedMapAcc - map.sourceRangeStart;
                newValue = map.destinationRangeStart + distance;
                return false;
            }, seedMapAcc);
            return newValue;
        }, groupedSeed[0] + i);

        if (!ans || newSeed < ans) ans = newSeed;
    }
})

console.log(ans);
