export const toTitleCase = str => {
    return str
        .replace('-', ' ')
        .replace(
            /([^\W_]+[^\s-]*) */g,
            txt => txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase()
        );
};

export const isUpperCase = str => {
    return str === str.toUpperCase();
};

export const isEmpty = str => {
    return !str || str.trim().length === 0;
};
