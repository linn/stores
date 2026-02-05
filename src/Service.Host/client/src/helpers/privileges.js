import config from '../config';

async function fetchPrivileges(employeeId) {
    const url = `${config.proxyRoot}/authorisation/permissions?who=/employees/${employeeId}`;
    const response = await fetch(url, {
        headers: {
            Accept: 'application/json'
        }
    });
    if (!response.ok) {
        throw new Error(`Failed to fetch privileges: ${response.status}`);
    }
    const data = await response.json();
    if (!Array.isArray(data)) {
        throw new Error('Unexpected response format');
    }
    return data.map(item => item.privilege);
}

export default fetchPrivileges;
