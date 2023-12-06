import { getLines, SeedMap, getSeedMaps, Map } from './shared';

const lines: string[] = getLines('./data/input.txt');
const seeds: bigint[] = lines[0].split(' ').slice(1).map((s: string) => BigInt(s));
const seedMaps: SeedMap[] = getSeedMaps(lines.splice(2));

const ans: bigint = seeds.reduce((ansAcc: bigint, seed: bigint): bigint => {
    const newSeed: bigint = seedMaps.reduce((seedMapAcc: bigint, seedMap: SeedMap): bigint => {
        return seedMap.maps.reduce((mapAcc: bigint, map: Map): bigint => {
            if (mapAcc != seedMapAcc)
                return mapAcc;

            if (mapAcc > map.sourceRangeStart + map.range - BigInt(1) || mapAcc < map.sourceRangeStart)
                return mapAcc;

            const distance: bigint =  mapAcc - map.sourceRangeStart;
            return map.destinationRangeStart + distance;
        }, seedMapAcc);
    }, seed);

    if (!ansAcc)
        return newSeed;

    if (newSeed < ansAcc) return newSeed;
    return ansAcc;
}, null);

console.log(ans);
