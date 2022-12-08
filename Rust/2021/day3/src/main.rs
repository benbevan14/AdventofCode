fn main() {
    let data: Vec<&str> = include_str!("../input.txt").lines().collect();
    // println!("part 1: {}", part1(&data));
    println!("part 2: {}", part2(&data));
}

fn part1(data: &Vec<&str>) -> i32 {
    let length = data[0].len();
    let mut totals: Vec<i32> = vec![0; length];
    for i in 0..length {
        totals[i] = data
            .iter()
            .map(|s| s[i..(i + 1)].parse::<i32>().unwrap())
            .sum();
    }

    let mut gamma = 0;
    let mut epsilon = 0;

    for i in 0..length {
        if totals[i] > data.len() as i32 / 2 {
            gamma += i32::pow(2, (length - i - 1) as u32);
        } else {
            epsilon += i32::pow(2, (length - i - 1) as u32);
        }
    }

    gamma * epsilon
}

fn part2(data: &Vec<&str>) -> i32 {
    let length = data[0].len();
    let mut ox = data.clone();
    let mut co2 = data.clone();
    for i in 0..length {
        if ox.len() > 1 {
            let length = ox.len() as i32;
            let ox_sum: i32 = ox
                .iter()
                .map(|s| s[i..(i + 1)].parse::<i32>().unwrap())
                .sum();

            match (ox_sum * 2).cmp(&length) {
                std::cmp::Ordering::Greater | std::cmp::Ordering::Equal => {
                    ox.retain(|line| &line[i..(i + 1)] == "1")
                }
                std::cmp::Ordering::Less => ox.retain(|line| &line[i..(i + 1)] == "0"),
            }
        }
        if co2.len() > 1 {
            let length = co2.len() as i32;
            let co2_sum: i32 = co2
                .iter()
                .map(|s| s[i..(i + 1)].parse::<i32>().unwrap())
                .sum();

            match (co2_sum * 2).cmp(&length) {
                std::cmp::Ordering::Greater | std::cmp::Ordering::Equal => {
                    co2.retain(|line| &line[i..(i + 1)] == "0")
                }
                std::cmp::Ordering::Less => co2.retain(|line| &line[i..(i + 1)] == "1"),
            }
        }
    }
    i32::from_str_radix(&ox.join(""), 2).unwrap() * i32::from_str_radix(&co2.join(""), 2).unwrap()
}
