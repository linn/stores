const config = window.APPLICATION_SETTINGS;
const defaultConfig = { appRoot: 'localhost:51698', proxyRoot: 'http://app.linn.co.uk' };

export default { ...defaultConfig, ...config };
