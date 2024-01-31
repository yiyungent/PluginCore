<template>
  <div>
    <div class="store-container">
      <div
        class="pluginstore-1"
        v-for="(plugin, index) in plugins"
        :key="index"
      >
        <div class="pluginstore-2">
          <div class="pluginstore-3">
            {{ plugin.displayName
            }}<svg
              aria-hidden="true"
              focusable="false"
              data-prefix="fas"
              data-icon="circle-check"
              role="img"
              xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 512 512"
              class="pluginstore-4"
            >
              <path
                fill="currentColor"
                d="M256 512A256 256 0 1 0 256 0a256 256 0 1 0 0 512zM369 209L241 337c-9.4 9.4-24.6 9.4-33.9 0l-64-64c-9.4-9.4-9.4-24.6 0-33.9s24.6-9.4 33.9 0l47 47L335 175c9.4-9.4 24.6-9.4 33.9 0s9.4 24.6 0 33.9z"
                class="pluginstore-5"
              ></path>
            </svg>
          </div>
          <div class="pluginstore-6">
            <svg
              aria-hidden="true"
              focusable="false"
              data-prefix="fas"
              data-icon="expand"
              role="img"
              xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 448 512"
              class="pluginstore-7"
            >
              <path
                fill="currentColor"
                d="M32 32C14.3 32 0 46.3 0 64v96c0 17.7 14.3 32 32 32s32-14.3 32-32V96h64c17.7 0 32-14.3 32-32s-14.3-32-32-32H32zM64 352c0-17.7-14.3-32-32-32s-32 14.3-32 32v96c0 17.7 14.3 32 32 32h96c17.7 0 32-14.3 32-32s-14.3-32-32-32H64V352zM320 32c-17.7 0-32 14.3-32 32s14.3 32 32 32h64v64c0 17.7 14.3 32 32 32s32-14.3 32-32V64c0-17.7-14.3-32-32-32H320zM448 352c0-17.7-14.3-32-32-32s-32 14.3-32 32v64H320c-17.7 0-32 14.3-32 32s14.3 32 32 32h96c17.7 0 32-14.3 32-32V352z"
                class="pluginstore-8"
              ></path>
            </svg>
          </div>
        </div>
        <div class="pluginstore-9">{{ plugin.description }}</div>
        <div class="resource-card-footer">
          <div class="resource-card-footer-tags">
            <span
              v-for="tag in plugin.tags"
              :key="tag"
              class="resource-card-footer-tag"
              >{{ tag }}</span
            >
          </div>
          <div class="resource-card-footer-divider"></div>
          <div class="resource-card-footer-info">
            <div class="resource-card-footer-group">
              <a
                :href="plugin.github"
                target="_blank"
                rel="noopener noreferrer"
                class="pluginstore-16"
                ><svg
                  aria-hidden="true"
                  focusable="false"
                  data-prefix="fab"
                  data-icon="github"
                  role="img"
                  xmlns="http://www.w3.org/2000/svg"
                  viewBox="0 0 496 512"
                  class="pluginstore-17"
                >
                  <path
                    fill="currentColor"
                    d="M165.9 397.4c0 2-2.3 3.6-5.2 3.6-3.3.3-5.6-1.3-5.6-3.6 0-2 2.3-3.6 5.2-3.6 3-.3 5.6 1.3 5.6 3.6zm-31.1-4.5c-.7 2 1.3 4.3 4.3 4.9 2.6 1 5.6 0 6.2-2s-1.3-4.3-4.3-5.2c-2.6-.7-5.5.3-6.2 2.3zm44.2-1.7c-2.9.7-4.9 2.6-4.6 4.9.3 2 2.9 3.3 5.9 2.6 2.9-.7 4.9-2.6 4.6-4.6-.3-1.9-3-3.2-5.9-2.9zM244.8 8C106.1 8 0 113.3 0 252c0 110.9 69.8 205.8 169.5 239.2 12.8 2.3 17.3-5.6 17.3-12.1 0-6.2-.3-40.4-.3-61.4 0 0-70 15-84.7-29.8 0 0-11.4-29.1-27.8-36.6 0 0-22.9-15.7 1.6-15.4 0 0 24.9 2 38.6 25.8 21.9 38.6 58.6 27.5 72.9 20.9 2.3-16 8.8-27.1 16-33.7-55.9-6.2-112.3-14.3-112.3-110.5 0-27.5 7.6-41.3 23.6-58.9-2.6-6.5-11.1-33.3 2.6-67.9 20.9-6.5 69 27 69 27 20-5.6 41.5-8.5 62.8-8.5s42.8 2.9 62.8 8.5c0 0 48.1-33.6 69-27 13.7 34.7 5.2 61.4 2.6 67.9 16 17.7 25.8 31.5 25.8 58.9 0 96.5-58.9 104.2-114.8 110.5 9.2 7.9 17 22.9 17 46.4 0 33.7-.3 75.4-.3 83.6 0 6.5 4.6 14.4 17.3 12.1C428.2 457.8 496 362.9 496 252 496 113.3 383.5 8 244.8 8zM97.2 352.9c-1.3 1-1 3.3.7 5.2 1.6 1.6 3.9 2.3 5.2 1 1.3-1 1-3.3-.7-5.2-1.6-1.6-3.9-2.3-5.2-1zm-10.8-8.1c-.7 1.3.3 2.9 2.3 3.9 1.6 1 3.6.7 4.3-.7.7-1.3-.3-2.9-2.3-3.9-2-.6-3.6-.3-4.3.7zm32.4 35.6c-1.6 1.3-1 4.3 1.3 6.2 2.3 2.3 5.2 2.6 6.5 1 1.3-1.3.7-4.3-1.3-6.2-2.2-2.3-5.2-2.6-6.5-1zm-11.4-14.7c-1.6 1-1.6 3.6 0 5.9 1.6 2.3 4.3 3.3 5.6 2.3 1.6-1.3 1.6-3.9 0-6.2-1.4-2.3-4-3.3-5.6-2z"
                    class="pluginstore-18"
                  ></path></svg></a
              ><a
                :href="plugin.homepageUrl"
                target="_blank"
                rel="noopener noreferrer"
                class="pluginstore-19"
                ><svg
                  aria-hidden="true"
                  focusable="false"
                  data-prefix="fas"
                  data-icon="cube"
                  role="img"
                  xmlns="http://www.w3.org/2000/svg"
                  viewBox="0 0 512 512"
                  class="pluginstore-20"
                >
                  <path
                    fill="currentColor"
                    d="M234.5 5.7c13.9-5 29.1-5 43.1 0l192 68.6C495 83.4 512 107.5 512 134.6V377.4c0 27-17 51.2-42.5 60.3l-192 68.6c-13.9 5-29.1 5-43.1 0l-192-68.6C17 428.6 0 404.5 0 377.4V134.6c0-27 17-51.2 42.5-60.3l192-68.6zM256 66L82.3 128 256 190l173.7-62L256 66zm32 368.6l160-57.1v-188L288 246.6v188z"
                    class="pluginstore-21"
                  ></path></svg
              ></a>
            </div>
            <div class="resource-card-footer-group">
              <div class="pluginstore-23">
                <div class="pluginstore-24">
                  <a
                    href="https://github.com/yanyongyu"
                    target="_blank"
                    rel="noopener noreferrer"
                    class="pluginstore-25"
                    ><img
                      src="https://github.com/yanyongyu.png?size=80"
                      class="pluginstore-26"
                  /></a>
                </div>
              </div>
              <span class="pluginstore-27">{{ plugin.author }}</span>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
// https://nonebot.dev/store/plugins

if (typeof window !== "undefined") window.global = window;

// import axios from "axios";
const axios = require("axios").default;

export default {
  data() {
    return {
      plugins: [],
    };
  },

  created() {
    console.log("PluginStore");

    const registryUrl =
      "https://ghproxy.net/https://raw.githubusercontent.com/yiyungent/PluginCore/main/plugins.json";
    axios.get(registryUrl).then((res) => {
      console.log(res.data);
      this.plugins = res.data.plugins;
    });
  },
};
</script>
<style scoped>
.store-container {
  display: grid;
  gap: 1.5rem;
  grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
  margin-bottom: 2rem;
  margin-top: 1rem;
}
.pluginstore-1 {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  background-color: hsl(0 0% 95%/1);
  border-color: hsl(0 0% 95%/1);
  border-radius: 0.5rem;
  border-width: 2px;
  display: flex;
  flex-direction: column;
  min-height: 12rem;
  padding: 1rem;
  row-gap: 0.5rem;
  transition-duration: 0.5s;
  transition-property: color, background-color, border-color,
    text-decoration-color, fill, stroke;
  transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
  width: 100%;
}

.pluginstore-1:focus {
  outline: 0px;
}

.pluginstore-1:hover {
  border-color: hsl(0 78% 62%/1);
}

.pluginstore-2 {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  align-items: center;
  display: flex;
  font-size: 1.125rem;
  font-weight: 500;
  line-height: 1.75rem;
  width: 100%;
}

.pluginstore-2:focus {
  outline: 0px;
}

.pluginstore-3 {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  flex-grow: 1;
  overflow: hidden;
  text-align: left;
  text-overflow: ellipsis;
  white-space: nowrap;
}

.pluginstore-3:focus {
  outline: 0px;
}

.pluginstore-4 {
  border: 0px solid rgb(229, 231, 235);
  vertical-align: -0.125em;
  display: inline-block;
  fill: currentcolor;
  color: hsl(158 64% 52%/1);
  height: 1.25rem;
  margin-left: 0.5rem;
  width: 1.25rem;
  box-sizing: content-box;
  overflow: visible;
}

.pluginstore-4:focus {
  outline: 0px;
}

.pluginstore-5 {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
}

.pluginstore-5:focus {
  outline: 0px;
}

.pluginstore-6 {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  fill: currentcolor;
  cursor: pointer;
  flex: 0 0 auto;
}

.pluginstore-6:focus {
  outline: 0px;
}

.pluginstore-7 {
  border: 0px solid rgb(229, 231, 235);
  vertical-align: -0.125em;
  height: 1em;
  display: inline-block;
  box-sizing: content-box;
  overflow: visible;
}

.pluginstore-7:focus {
  outline: 0px;
}

.pluginstore-8 {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
}

.pluginstore-8:focus {
  outline: 0px;
}

.pluginstore-9 {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  flex: 1 1 0%;
  font-size: 0.875rem;
  line-height: 1.25rem;
  overflow-wrap: break-word;
  text-overflow: ellipsis;
  width: 100%;
}

.pluginstore-9:focus {
  outline: 0px;
}

.resource-card-footer {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  cursor: default;
  display: flex;
  flex-direction: column;
  width: 100%;
}

.resource-card-footer:focus {
  outline: 0px;
}

.resource-card-footer-tags {
  box-sizing: border-box;
  border: 0px solid rgb(14, 94, 255);
  display: flex;
  flex-wrap: wrap;
  gap: 0.25rem;
}

.resource-card-footer-tags:focus {
  outline: 0px;
}

.resource-card-footer-tag {
  box-sizing: border-box;
  border: 0px solid rgb(14, 94, 255);
  cursor: pointer;
  font-family: ui-monospace, SFMono-Regular, Menlo, Monaco, Consolas,
    "Liberation Mono", "Courier New", monospace;
  align-items: center;
  border-radius: 0.25rem;
  display: inline-flex;
  font-size: 0.75rem;
  height: 1.25rem;
  justify-content: center;
  line-height: 1rem;
  padding-left: 0.5rem;
  padding-right: 0.5rem;
  width: fit-content;
  background-color: rgb(78, 201, 176);
  color: rgb(255, 255, 255);
}

.resource-card-footer-tag:focus {
  outline: 0px;
}

.resource-card-footer-divider {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  margin: 0px;
  align-items: center;
  align-self: stretch;
  display: flex;
  flex-direction: row;
  height: 1rem;
  margin-bottom: 1rem;
  margin-top: 1rem;
  white-space: nowrap;
}

.resource-card-footer-divider:focus {
  outline: 0px;
}

.resource-card-footer-divider:before {
  content: "";
  flex-grow: 1;
  height: 0.125rem;
  width: 100%;
  background-color: #dcdddf;
}

.resource-card-footer-divider:after {
  content: "";
  flex-grow: 1;
  height: 0.125rem;
  width: 100%;
  background-color: #dcdddf;
}

.resource-card-footer-info {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  align-items: center;
  display: flex;
  justify-content: space-between;
  width: 100%;
}

.resource-card-footer-info:focus {
  outline: 0px;
}

.resource-card-footer-group {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  align-items: center;
  column-gap: 0.25rem;
  display: flex;
  justify-content: center;
  line-height: 1;
}

.resource-card-footer-group:focus {
  outline: 0px;
}

.pluginstore-16 {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  color: inherit;
  text-decoration: inherit;
}

.pluginstore-16:focus {
  outline: 0px;
}

.pluginstore-17 {
  border: 0px solid rgb(229, 231, 235);
  vertical-align: -0.125em;
  display: inline-block;
  fill: currentcolor;
  height: 1.25rem;
  opacity: 0.8;
  width: 1.25rem;
  box-sizing: content-box;
  overflow: visible;
}

.pluginstore-17:focus {
  outline: 0px;
}

.pluginstore-17:hover {
  opacity: 1;
}

.pluginstore-18 {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
}

.pluginstore-18:focus {
  outline: 0px;
}

.pluginstore-19 {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  color: inherit;
  text-decoration: inherit;
}

.pluginstore-19:focus {
  outline: 0px;
}

.pluginstore-20 {
  border: 0px solid rgb(229, 231, 235);
  vertical-align: -0.125em;
  display: inline-block;
  fill: currentcolor;
  height: 1.25rem;
  opacity: 0.8;
  width: 1.25rem;
  box-sizing: content-box;
  overflow: visible;
}

.pluginstore-20:focus {
  outline: 0px;
}

.pluginstore-20:hover {
  opacity: 1;
}

.pluginstore-21 {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
}

.pluginstore-21:focus {
  outline: 0px;
}

.pluginstore-23 {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  position: relative;
  display: inline-flex;
}

.pluginstore-23:focus {
  outline: 0px;
}

.pluginstore-24 {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  border-radius: 9999px;
  height: 1.25rem;
  transition-duration: 0.15s;
  transition-property: box-shadow;
  transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
  width: 1.25rem;
  aspect-ratio: 1 / 1;
  display: block;
  overflow: hidden;
}

.pluginstore-24:focus {
  outline: 0px;
}

.pluginstore-24:hover {
  box-shadow: 0 0 #0000, 0 0 #0000, 0 0 #0000;
}

.pluginstore-25 {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  color: inherit;
  text-decoration: inherit;
}

.pluginstore-25:focus {
  outline: 0px;
}

.pluginstore-26 {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  display: block;
  vertical-align: middle;
  max-width: 100%;
  height: 100%;
  object-fit: cover;
  width: 100%;
}

.pluginstore-26:focus {
  outline: 0px;
}

.pluginstore-27 {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  cursor: pointer;
  font-size: 0.875rem;
  line-height: 1.25rem;
}

.pluginstore-27:focus {
  outline: 0px;
}
</style>
